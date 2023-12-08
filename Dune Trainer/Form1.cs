using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Native;
using Dune_Trainer.Common;
using Dune_Trainer.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dune_Trainer
{
    public partial class Form1 : Form
    {
        private readonly VehicleManager vehicleManager;
        private readonly BuildingManager buildingManager;
        private readonly MemorySharp memory;
        private IntPtr unitPointer;
        private Dictionary<IntPtr, Thread> invincibleThreads;

        public Form1(MemoryService memoryService)
        {
            InitializeComponent();
            this.invincibleThreads = new Dictionary<IntPtr, Thread>();
            this.vehicleManager = new VehicleManager();
            this.buildingManager = new BuildingManager();
            this.unitPointer = (IntPtr)0x7988A0;

            label1.Text = "test";
            this.memory = memoryService.GetMemory();

            //typePtr = typePtr - (0x94*220);
            //this.memory.Write<byte>(typePtr, 12, false);

            var unitTypesColumn = dataGridView1.Columns[1];
            foreach (var type in vehicleManager.GetTypes()) {
                (unitTypesColumn as DataGridViewComboBoxColumn).Items.Add(type.Value);
            }
            var buildingTypesColumn = dataGridView1.Columns[2];
            foreach (var type in buildingManager.GetTypes())
            {
                (buildingTypesColumn as DataGridViewComboBoxColumn).Items.Add(type.Value);
            }
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;

            UpdateTable();

        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();

            var typePtr = this.unitPointer;

            var vehicleTypes = this.vehicleManager.GetTypes();
            var buildingTypes = this.buildingManager.GetTypes();


            for (int i = 0; i < 1000; i++)
            {
                //typePtr = typePtr + (0x94 * i);

                byte unitType = this.memory.Read<byte>(typePtr + 0x90 + (0x94 * i), 1, false)[0];
                IntPtr pointer = typePtr + (0x94 * i);
                byte type;
                int health = 0;
                string unitTypeText = "";
                string buildingTypeText = "";

                if (unitType == 2)
                {
                    type = this.memory.Read<byte>(typePtr + 0x34 + (0x94 * i), 1, false)[0];
                    health = this.memory.Read<int>(typePtr + 0x24 + (0x94 * i), 1, false)[0];
                    buildingTypeText = buildingTypes[type];
                } 
                if (unitType == 1)
                {
                    type = this.memory.Read<byte>(typePtr + 0x18 + (0x94 * i), 1, false)[0];
                    health = this.memory.Read<int>(typePtr + 0x4 + (0x94 * i), 1, false)[0];
                    unitTypeText = vehicleTypes[type];
                }
                if(unitType == 1 || unitType == 2)
                {
                    dataGridView1.Rows.Add(pointer, unitTypeText, buildingTypeText, health);
                }
            }
        }

        private void UpdateInvincibleTable()
        {
            dataGridView2.Rows.Clear();

            foreach (IntPtr key in invincibleThreads.Keys)
            {
                dataGridView2.Rows.Add(key);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void updateTable_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var pointer = (IntPtr)dataGridView2.SelectedCells[0].OwningRow.Cells[0].Value;
                invincibleThreads.Remove(pointer);
                UpdateInvincibleTable();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IntPtr typePtr = (IntPtr)0x7B452C; // Atreides (3rd array, first 0x7988A0)
            //IntPtr typePtr = (IntPtr)0x7DAEBC; // Harkonnen
            //IntPtr typePtr = (IntPtr)0x80184C; // Ordos
            //IntPtr typePtr = (IntPtr)0x8281DC; // Emperor
            //IntPtr typePtr = (IntPtr)0x84EB6C; // Fremen
            //IntPtr typePtr = (IntPtr)0x8754FC; // Smugglers
            //IntPtr typePtr = (IntPtr)0x89BE8C; // Mercenaries
            //IntPtr typePtr = (IntPtr)0x8C281C; // Sandworm
            this.unitPointer = getUnitPointer(comboBox1.SelectedIndex);
            UpdateTable();
        }

        private IntPtr getUnitPointer(int index)
        {
            var unitPointer = (IntPtr)0x000000;
            switch (index)
            {
                case 0:
                    unitPointer = (IntPtr)0x7988A0;
                    break;
                case 1:
                    unitPointer = (IntPtr)0x7BF230;
                    break;
                case 2:
                    unitPointer = (IntPtr)0x7E5BC0;
                    break;
                case 3:
                    unitPointer = (IntPtr)0x80C550;
                    break;
                case 4:
                    unitPointer = (IntPtr)0x832EE0;
                    break;
                case 5:
                    unitPointer = (IntPtr)0x859870;
                    break;
                case 6:
                    unitPointer = (IntPtr)0x880200;
                    break;
                case 7:
                    unitPointer = (IntPtr)0x8A6B90;
                    break;
            }

            return unitPointer;
        }

        private void invincibleThread(IntPtr healthPointer)
        {
            var health = this.memory.Read<int>(healthPointer, 1, false)[0];
            while (true)
            {
                if (!this.invincibleThreads.ContainsKey(healthPointer))
                {
                    break;
                }
                this.memory.Write<int>(healthPointer, health, false);
                Thread.Sleep(25);
            }
        }

        private void changeType(DataGridView dataGridView)
        {
            IntPtr pointer = (IntPtr)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;

            var vehicleTypes = this.vehicleManager.GetTypes();
            var buildingTypes = this.buildingManager.GetTypes();

            byte unitType = this.memory.Read<byte>(pointer + 0x90, 1, false)[0];
            var health = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[3].Value);

            if (unitType == 2)
            {
                var type = buildingTypes.FirstOrDefault(x => x.Value == (string)dataGridView1.SelectedCells[0].OwningRow.Cells[2].Value).Key;
                this.memory.Write<byte>(pointer + 0x34, type, false);
                this.memory.Write<int>(pointer + 0x24, health, false);
            }
            if (unitType == 1)
            {
                var type = vehicleTypes.FirstOrDefault(x => x.Value == (string)dataGridView1.SelectedCells[0].OwningRow.Cells[1].Value).Key;
                this.memory.Write<byte>(pointer + 0x18, type, false);
                this.memory.Write<int>(pointer + 0x4, health, false);
            }
            UpdateTable();
        }

        private int getIndexByHouseName(string houseName)
        {
            switch (houseName)
            {
                case "Atreides":
                    return 0;
                case "Harkonnen":
                    return 1;
                case "Ordos":
                    return 2;
            }
            return 0;
        }
        private void moveUnit()
        {
            IntPtr pointer = (IntPtr)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;

            IntPtr unitPointer = getUnitPointer(getIndexByHouseName((string)dataGridView1.SelectedCells[0].OwningRow.Cells[6].Value));

            byte mapXSize = this.memory.Read<byte>((IntPtr)0x517DE8, 1, false)[0];
            byte mapYSize = this.memory.Read<byte>((IntPtr)0x517DEC, 1, false)[0];
            var nextSlotBytes = this.memory.Read<byte>(unitPointer - 0x2C, 3, false);
            var lastSlotBytes = this.memory.Read<byte>(unitPointer - 0x24, 3, false);
            byte[] vehicleData = this.memory.Read<byte>(pointer, 148, false);

            byte[] nextSlotBytesTemporary = { nextSlotBytes[0], nextSlotBytes[1], nextSlotBytes[2], 0 };
            // Convert byte[] to integer
            int nextSlotPointerInt = BitConverter.ToInt32(nextSlotBytesTemporary, 0);
            // Create IntPtr from the integer value
            IntPtr nextSlotPointer = new IntPtr(nextSlotPointerInt);
            //IntPtr nextSlotPointer = findNextSlot();
            byte[] nextSlotPointerBytes = BitConverter.GetBytes(nextSlotPointer.ToInt32());

            byte[] lastSlotBytesTemporary = { lastSlotBytes[0], lastSlotBytes[1], lastSlotBytes[2], 0 };
            // Convert byte[] to integer
            int lastSlotPointerInt = BitConverter.ToInt32(lastSlotBytesTemporary, 0);
            IntPtr lastSlotPointer = new IntPtr(lastSlotPointerInt);

            //byte[] previousVehicleData = this.memory.Read<byte>(nextSlotPointer - 0x94 + 0x7C, 3, false);
            //byte[] prePreviousVehicleData = this.memory.Read<byte>(nextSlotPointer - 0x94 * 2 + 0x7C, 3, false);

            for (int key = 0; key < vehicleData.Length; ++key)
            {
                if (key != 141 && key != 140 && key != 128 && key != 129 && key != 130 && key != 124 && key != 125 && key != 126 && key != 58 && key != 73 && key != 75 && key != 77 && key != 81)
                {
                    this.memory.Write<byte>(nextSlotPointer + key, vehicleData[key], false);
                }
            }

            IntPtr secondNextSlotPointer = findNextSlotFromPointer(nextSlotPointer);
            byte[] secondNextSlotPointerBytes = BitConverter.GetBytes(secondNextSlotPointer.ToInt32());

            byte positionX = (byte)(vehicleData[72]);
            byte positionY = (byte)(vehicleData[73] + 1);
            byte positionYGrid = 16;
            byte positionYGridCount = vehicleData[59];

            if (vehicleData[58] <= 223)
            {
                positionYGrid = (byte)(vehicleData[58] + 32);
            }
            else
            {
                positionYGridCount++;
            }

            IntPtr mapAddress = (IntPtr)0x517DF4 + (0xC * positionX) + (0xC * (positionY * mapXSize));


            //this.memory.Write<byte>(nextSlotPointer + 0x3A - 0x4, 0, false); // horisontal . starting 16, each step 32
            this.memory.Write<byte>(nextSlotPointer + 0x3A, positionYGrid, false); // 58 key vertical ,  starting 16, each step 32
            this.memory.Write<byte>(nextSlotPointer + 0x3B, positionYGridCount, false); // vertical bytes count
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2 + 0x2, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2 + 0x2 + 0x4, positionY, false);

            this.memory.Write<byte>(lastSlotPointer + 0x7C, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(lastSlotPointer + 0x7D, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(lastSlotPointer + 0x7E, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(nextSlotPointer + 0x7C, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x7D, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x7E, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x80, lastSlotBytes[0], false);
            this.memory.Write<byte>(nextSlotPointer + 0x81, lastSlotBytes[1], false);
            this.memory.Write<byte>(nextSlotPointer + 0x82, lastSlotBytes[2], false);

            this.memory.Write<byte>(unitPointer - 0x2C, secondNextSlotPointerBytes[0], false);
            this.memory.Write<byte>(unitPointer - 0x2B, secondNextSlotPointerBytes[1], false);
            this.memory.Write<byte>(unitPointer - 0x2A, secondNextSlotPointerBytes[2], false);

            this.memory.Write<byte>(unitPointer + 0x26080, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(unitPointer + 0x26081, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(unitPointer + 0x26082, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(unitPointer - 0x24, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(unitPointer - 0x23, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(unitPointer - 0x22, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(secondNextSlotPointer + 0x7C, 0, false);
            this.memory.Write<byte>(secondNextSlotPointer + 0x7D, 0, false);
            this.memory.Write<byte>(secondNextSlotPointer + 0x7E, 0, false);


            byte[] unitMapType = BitConverter.GetBytes(8 + getIndexByHouseName((string)dataGridView1.SelectedCells[0].OwningRow.Cells[6].Value));

            this.memory.Write<byte>(mapAddress, unitMapType[0], false); // Write to map

            UpdateTable();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                var dataGridView = sender as DataGridView;
                changeType(dataGridView);
            }
            if(e.ColumnIndex == 6)
            {
                moveUnit();
            }
        }

        private void AddInvincibleButtonClick()
        {
            var pointer = (IntPtr)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;
            byte unitType = this.memory.Read<byte>(pointer + 0x90, 1, false)[0];
            var health = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[3].Value);
            IntPtr healthPointer = pointer;
            bool isUnitOrBuilding = false;
            if (unitType == 2)
            {
                healthPointer = pointer + 0x24;
                isUnitOrBuilding = true;
            }
            if (unitType == 1)
            {
                healthPointer = pointer + 0x4;
                isUnitOrBuilding = true;
            }
            if (isUnitOrBuilding && !this.invincibleThreads.ContainsKey(healthPointer))
            {
                var thread = new Thread(() => invincibleThread(healthPointer));
                invincibleThreads.Add(healthPointer, thread);
                thread.Start();
                UpdateInvincibleTable();
            }
        }

        private IntPtr findNextSlot()
        {
            for (int i = 999; i >= 0; i--)
            {
                byte unitType = this.memory.Read<byte>(this.unitPointer + 0x94 * i + 0x90, 1, false)[0];
                if(unitType == 0)
                {
                    return this.unitPointer + 0x94 * i;
                }
            }
            return (IntPtr)0x000000;
        }

        private IntPtr findNextSlotFromPointer(IntPtr pointer)
        {
            for (int i = 1; i <= 1000; i++)
            {
                byte unitType = this.memory.Read<byte>(pointer - 0x94 * i + 0x90, 1, false)[0];
                if (unitType == 0)
                {
                    return pointer - 0x94 * i;
                }
            }
            return (IntPtr)0x000000;
        }

        private void copyUnit()
        {
            var pointer = (IntPtr)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;

            byte mapXSize = this.memory.Read<byte>((IntPtr)0x517DE8, 1, false)[0];
            byte mapYSize = this.memory.Read<byte>((IntPtr)0x517DEC, 1, false)[0];
            var nextSlotBytes = this.memory.Read<byte>(this.unitPointer - 0x2C, 3, false);
            var lastSlotBytes = this.memory.Read<byte>(this.unitPointer - 0x24, 3, false);
            byte[] vehicleData = this.memory.Read<byte>(pointer, 148, false);

            byte[] nextSlotBytesTemporary = { nextSlotBytes[0], nextSlotBytes[1], nextSlotBytes[2], 0 };
            // Convert byte[] to integer
            int nextSlotPointerInt = BitConverter.ToInt32(nextSlotBytesTemporary, 0);
            // Create IntPtr from the integer value
            IntPtr nextSlotPointer = new IntPtr(nextSlotPointerInt);
            //IntPtr nextSlotPointer = findNextSlot();
            byte[] nextSlotPointerBytes = BitConverter.GetBytes(nextSlotPointer.ToInt32());

            byte[] lastSlotBytesTemporary = { lastSlotBytes[0], lastSlotBytes[1], lastSlotBytes[2], 0 };
            // Convert byte[] to integer
            int lastSlotPointerInt = BitConverter.ToInt32(lastSlotBytesTemporary, 0);
            IntPtr lastSlotPointer = new IntPtr(lastSlotPointerInt);

            //byte[] previousVehicleData = this.memory.Read<byte>(nextSlotPointer - 0x94 + 0x7C, 3, false);
            //byte[] prePreviousVehicleData = this.memory.Read<byte>(nextSlotPointer - 0x94 * 2 + 0x7C, 3, false);

            for (int key = 0; key < vehicleData.Length; ++key)
            {
                if (key != 141 && key != 140 && key != 128 && key != 129 && key != 130 && key != 124 && key != 125 && key != 126 && key != 58 && key != 73 && key != 75 && key != 77 && key != 81)
                {
                    this.memory.Write<byte>(nextSlotPointer + key, vehicleData[key], false);
                }
            }

            IntPtr secondNextSlotPointer = findNextSlotFromPointer(nextSlotPointer);
            byte[] secondNextSlotPointerBytes = BitConverter.GetBytes(secondNextSlotPointer.ToInt32());

            byte positionX = (byte)(vehicleData[72]);
            byte positionY = (byte)(vehicleData[73] + 1);
            byte positionYGrid = 16;
            byte positionYGridCount = vehicleData[59];

            if (vehicleData[58] <= 223)
            {
                positionYGrid = (byte)(vehicleData[58] + 32);
            }
            else
            {
                positionYGridCount++;
            }

            IntPtr mapAddress = (IntPtr)0x517DF4 + (0xC * positionX) + (0xC * (positionY * mapXSize));


            //this.memory.Write<byte>(nextSlotPointer + 0x3A - 0x4, 0, false); // horisontal . starting 16, each step 32
            this.memory.Write<byte>(nextSlotPointer + 0x3A, positionYGrid, false); // 58 key vertical ,  starting 16, each step 32
            this.memory.Write<byte>(nextSlotPointer + 0x3B, positionYGridCount, false); // vertical bytes count
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2 + 0x2, positionY, false);
            this.memory.Write<byte>(nextSlotPointer + 0x3A + 0xF + 0x2 + 0x2 + 0x4, positionY, false);

            this.memory.Write<byte>(lastSlotPointer + 0x7C, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(lastSlotPointer + 0x7D, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(lastSlotPointer + 0x7E, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(nextSlotPointer + 0x7C, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x7D, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x7E, 0, false);
            this.memory.Write<byte>(nextSlotPointer + 0x80, lastSlotBytes[0], false);
            this.memory.Write<byte>(nextSlotPointer + 0x81, lastSlotBytes[1], false);
            this.memory.Write<byte>(nextSlotPointer + 0x82, lastSlotBytes[2], false);

            this.memory.Write<byte>(this.unitPointer - 0x2C, secondNextSlotPointerBytes[0], false);
            this.memory.Write<byte>(this.unitPointer - 0x2B, secondNextSlotPointerBytes[1], false);
            this.memory.Write<byte>(this.unitPointer - 0x2A, secondNextSlotPointerBytes[2], false);

            this.memory.Write<byte>(this.unitPointer + 0x26080, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(this.unitPointer + 0x26081, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(this.unitPointer + 0x26082, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(this.unitPointer - 0x24, nextSlotPointerBytes[0], false);
            this.memory.Write<byte>(this.unitPointer - 0x23, nextSlotPointerBytes[1], false);
            this.memory.Write<byte>(this.unitPointer - 0x22, nextSlotPointerBytes[2], false);

            this.memory.Write<byte>(secondNextSlotPointer + 0x7C, 0, false);
            this.memory.Write<byte>(secondNextSlotPointer + 0x7D, 0, false);
            this.memory.Write<byte>(secondNextSlotPointer + 0x7E, 0, false);


            byte[] unitMapType = BitConverter.GetBytes(8 + comboBox1.SelectedIndex);

            this.memory.Write<byte>(mapAddress, unitMapType[0], false); // Write to map

            UpdateTable();
        }

        private void CopyButtonClicked()
        {
            var pointer = (IntPtr)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;
            byte unitType = this.memory.Read<byte>(pointer + 0x90, 1, false)[0];
            if(unitType == 1)
            {
                copyUnit();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 4:
                        AddInvincibleButtonClick();
                        break;
                    case 5:
                        CopyButtonClicked();
                        break;
                }
            }
        }
    }
}
