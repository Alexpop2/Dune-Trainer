using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Native;
using Dune_Trainer.Common;
using Dune_Trainer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            this.unitPointer = (IntPtr)0x7B452C;

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

            /*
            for (int i = 0; i <= 148; i++)
            {
                var column = new DataGridViewColumn();
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView2.Columns.Add(column);
            }

            AddVehicleData((IntPtr)0x7E2DBC - 0x94);
            for (int i = 0; i < 40; i++)
            {
                AddVehicleData((IntPtr)0x7E2DBC - 0x94*30 + 0x94 * i);
            }*/
        }

        /*private void AddVehicleData(IntPtr typePtr)
        {
            byte[] vehicleData = this.memory.Read<byte>(typePtr, 148, false);
            var row = new DataGridViewRow();
            foreach (var b in vehicleData)
            {
                var cell = new DataGridViewTextBoxCell();
                cell.Value = b;
                row.Cells.Add(cell);
            }
            dataGridView2.Rows.Add(row);
        }*/

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();

            //IntPtr typePtr = (IntPtr)0x7B452C; // Atreides
            //IntPtr typePtr = (IntPtr)0x7DAEBC; // Harkonnen
            //IntPtr typePtr = (IntPtr)0x80184C; // Ordos
            //IntPtr typePtr = (IntPtr)0x8281DC; // Emperor
            var typePtr = this.unitPointer;

            var vehicleTypes = this.vehicleManager.GetTypes();
            var buildingTypes = this.buildingManager.GetTypes();


            for (int i = 0; i < 232; i++)
            {
                //typePtr = typePtr + (0x94 * i);

                byte unitFill = this.memory.Read<byte>(typePtr + 0x60 + (0x94 * i), 1, false)[0];
                byte pointer = this.memory.Read<byte>(typePtr + (0x94 * i), 1, false)[0];
                byte type;
                int health;
                string typeStr;
                string unitType = "";
                string buildingType = "";

                if (unitFill == 0)
                {
                    type = this.memory.Read<byte>(typePtr + 0x3C + (0x94 * i), 1, false)[0];
                    health = this.memory.Read<int>(typePtr + 0x2C + (0x94 * i), 1, false)[0];
                    typeStr = buildingTypes[type];
                    buildingType = buildingTypes[type];
                } else
                {
                    type = this.memory.Read<byte>(typePtr + 0x20 + (0x94 * i), 1, false)[0];
                    health = this.memory.Read<int>(typePtr + 0xC + (0x94 * i), 1, false)[0];
                    typeStr = vehicleTypes[type];
                    unitType = vehicleTypes[type];
                }
                if(health > 0)
                {
                    dataGridView1.Rows.Add(pointer, unitType, buildingType, health);
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

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IntPtr typePtr = (IntPtr)0x7B452C; // Atreides (3rd array, first 0x79889D)
            //IntPtr typePtr = (IntPtr)0x7DAEBC; // Harkonnen
            //IntPtr typePtr = (IntPtr)0x80184C; // Ordos
            //IntPtr typePtr = (IntPtr)0x8281DC; // Emperor
            //IntPtr typePtr = (IntPtr)0x84EB6C; // Fremen
            //IntPtr typePtr = (IntPtr)0x8754FC; // Smugglers
            //IntPtr typePtr = (IntPtr)0x89BE8C; // Mercenaries
            //IntPtr typePtr = (IntPtr)0x8C281C; // Sandworm
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.unitPointer = (IntPtr)0x7B452C;
                    break;
                case 1:
                    this.unitPointer = (IntPtr)0x7DAEBC;
                    break;
                case 2:
                    this.unitPointer = (IntPtr)0x80184C;
                    break;
                case 3:
                    this.unitPointer = (IntPtr)0x8281DC;
                    break;
                case 4:
                    this.unitPointer = (IntPtr)0x84EB6C;
                    break;
                case 5:
                    this.unitPointer = (IntPtr)0x8754FC;
                    break;
                case 6:
                    this.unitPointer = (IntPtr)0x89BE8C;
                    break;
                case 7:
                    this.unitPointer = (IntPtr)0x8C281C;
                    break;
            }
            UpdateTable();
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var pointer = (byte)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;


            var typePtr = this.unitPointer;

            var vehicleTypes = this.vehicleManager.GetTypes();
            var buildingTypes = this.buildingManager.GetTypes();

            byte unitFill = this.memory.Read<byte>(typePtr + 0x60 + (0x94 * pointer), 1, false)[0];
            var health = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[3].Value);

            if (unitFill == 0)
            {
                var type = buildingTypes.FirstOrDefault(x => x.Value == (string)dataGridView1.SelectedCells[0].OwningRow.Cells[2].Value).Key;
                this.memory.Write<byte>(typePtr + 0x3C + (0x94 * pointer), type, false);
                this.memory.Write<int>(typePtr + 0x2C + (0x94 * pointer), health, false);
                //type = this.memory.Read<byte>(typePtr + 0x3C + (0x94 * pointer), 1, false)[0];
                //health = this.memory.Read<int>(typePtr + 0x2C + (0x94 * pointer), 1, false)[0];
                //typeStr = buildingTypes[type];
                //buildingType = buildingTypes[type];
            }
            else
            {
                var type = vehicleTypes.FirstOrDefault(x => x.Value == (string)dataGridView1.SelectedCells[0].OwningRow.Cells[1].Value).Key;
                this.memory.Write<byte>(typePtr + 0x20 + (0x94 * pointer), type, false);
                this.memory.Write<int>(typePtr + 0xC + (0x94 * pointer), health, false);
                //type = this.memory.Read<byte>(typePtr + 0x20 + (0x94 * pointer), 1, false)[0];
                //health = this.memory.Read<int>(typePtr + 0xC + (0x94 * pointer), 1, false)[0];
                //typeStr = vehicleTypes[type];
                //unitType = vehicleTypes[type];
            }
            UpdateTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var pointer = (byte)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;
                var typePtr = this.unitPointer;
                byte unitFill = this.memory.Read<byte>(typePtr + 0x60 + (0x94 * pointer), 1, false)[0];
                var health = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells[3].Value);
                IntPtr healthPointer;
                if (unitFill == 0)
                {
                    //this.memory.Write<byte>(typePtr + 0x3C + (0x94 * pointer), type, false);
                    //this.memory.Write<int>(typePtr + 0x2C + (0x94 * pointer), health, false);
                    //type = this.memory.Read<byte>(typePtr + 0x3C + (0x94 * pointer), 1, false)[0];
                    //health = this.memory.Read<int>(typePtr + 0x2C + (0x94 * pointer), 1, false)[0];
                    //typeStr = buildingTypes[type];
                    //buildingType = buildingTypes[type];
                    healthPointer = typePtr + 0x2C + (0x94 * pointer);
                }
                else
                {
                    //this.memory.Write<byte>(typePtr + 0x20 + (0x94 * pointer), type, false);
                    //this.memory.Write<int>(typePtr + 0xC + (0x94 * pointer), health, false);
                    //type = this.memory.Read<byte>(typePtr + 0x20 + (0x94 * pointer), 1, false)[0];
                    //health = this.memory.Read<int>(typePtr + 0xC + (0x94 * pointer), 1, false)[0];
                    //typeStr = vehicleTypes[type];
                    //unitType = vehicleTypes[type];

                    healthPointer = typePtr + 0xC + (0x94 * pointer);
                }
                if(!this.invincibleThreads.ContainsKey(healthPointer))
                {
                    var thread = new Thread(() => invincibleThread(healthPointer));
                    invincibleThreads.Add(healthPointer, thread);
                    thread.Start();
                    UpdateInvincibleTable();
                }
            }
        }
    }
}
