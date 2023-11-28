using Binarysharp.MemoryManagement;
using Dune_Trainer.Common;
using Dune_Trainer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dune_Trainer
{
    public partial class UnitListTest : Form
    {

        private readonly VehicleManager vehicleManager;
        private readonly BuildingManager buildingManager;
        private readonly MemorySharp memory;
        private IntPtr unitPointer;
        private Dictionary<IntPtr, Thread> invincibleThreads;
        private byte[] readTest;
        public UnitListTest(MemoryService memoryService)
        {
            InitializeComponent();

            this.invincibleThreads = new Dictionary<IntPtr, Thread>();
            this.vehicleManager = new VehicleManager();
            this.buildingManager = new BuildingManager();
            this.unitPointer = (IntPtr)0x7B452C; // 0x7988A0
            this.memory = memoryService.GetMemory();


            for (int i = 0; i <= 148; i++)
            {
                var column = new DataGridViewColumn();
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }

            for (int i = 0; i < 1000; i++)
            {
                AddVehicleData((IntPtr)0x7988A0 + 0x94 * i);
            }

        }
        private void AddVehicleData(IntPtr typePtr)
        {
            byte[] vehicleData = this.memory.Read<byte>(typePtr, 148, false);
            var row = new DataGridViewRow();
            var cell2 = new DataGridViewTextBoxCell();
            cell2.Value = typePtr;
            row.Cells.Add(cell2);
            foreach (var b in vehicleData)
            {
                var cell = new DataGridViewTextBoxCell();
                cell.Value = b;
                row.Cells.Add(cell);
            }
            dataGridView1.Rows.Add(row);
        }

        private void UnitListTest_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var unitPointer = (IntPtr)0x7BCA2C; //0x7BC58C

            byte[] vehicleData = this.memory.Read<byte>(unitPointer - 0x94 * 50, 148, false);
            byte[] vehicleData2 = this.memory.Read<byte>(unitPointer, 148, false);


            byte[] vehicleData3 = this.memory.Read<byte>(unitPointer - 0x94*2 + 0x7C, 3, false);
            byte[] vehicleData4 = this.memory.Read<byte>(unitPointer - 0x94 * 3 + 0x7C, 3, false);
            /*for (int key = 0; key < vehicleData.Length; ++key)
            {
                if (key != 144 && key != 143)
                {

                    this.memory.Write<byte>((IntPtr)0x7BC589 + key, vehicleData[key], false);
                }

            }*/
            //0x7BC589
            //byte[] vehicleData = this.memory.Read<byte>((IntPtr)0x7BC589, 148, false);
            for (int key = 0; key < vehicleData2.Length; ++key)
            {
                if(key != 141 && key != 140 && key != 128 && key != 129 && key != 130 && key != 124 && key != 125 && key != 126 && key != 58 && key != 73 && key != 75 && key != 77 && key != 81)
                {

                    this.memory.Write<byte>(unitPointer - 0x94 * 1 + key, vehicleData2[key], false);
                }   
            }

            this.memory.Write<byte>(unitPointer - 0x94 + 0x3A, 240, false);
            this.memory.Write<byte>(unitPointer - 0x94 + 0x3A + 0xF, 7, false);
            this.memory.Write<byte>(unitPointer - 0x94 + 0x3A + 0xF + 0x2, 7, false);
            this.memory.Write<byte>(unitPointer - 0x94 + 0x3A + 0xF + 0x2 + 0x2, 7, false);
            this.memory.Write<byte>(unitPointer - 0x94 + 0x3A + 0xF + 0x2 + 0x2 + 0x4, 7, false);

            this.memory.Write<byte>(unitPointer + 0x7C, vehicleData3[0], false);
            this.memory.Write<byte>(unitPointer + 0x7D, vehicleData3[1], false);
            this.memory.Write<byte>(unitPointer + 0x7E, vehicleData3[2], false);

            this.memory.Write<byte>(unitPointer - 0x94 * 1 + 0x80, 44, false);
            this.memory.Write<byte>(unitPointer - 0x94 * 1 + 0x81, 202, false);
            this.memory.Write<byte>(unitPointer - 0x94 * 1 + 0x82, 123, false);

            this.memory.Write<byte>((IntPtr)0x798874, vehicleData4[0], false);
            this.memory.Write<byte>((IntPtr)0x798875, vehicleData4[1], false);
            this.memory.Write<byte>((IntPtr)0x798876, vehicleData4[2], false);

            this.memory.Write<byte>((IntPtr)0x7BE920, vehicleData3[0], false);
            this.memory.Write<byte>((IntPtr)0x7BE921, vehicleData3[1], false);
            this.memory.Write<byte>((IntPtr)0x7BE922, vehicleData3[2], false);

            this.memory.Write<byte>((IntPtr)0x79887C, vehicleData3[0], false);
            this.memory.Write<byte>((IntPtr)0x79887D, vehicleData3[1], false);
            this.memory.Write<byte>((IntPtr)0x79887E, vehicleData3[2], false);

            this.memory.Write<byte>(unitPointer - 0x94 * 2 + 0x7C, 0, false);
            this.memory.Write<byte>(unitPointer - 0x94 * 2 + 0x7D, 0, false);
            this.memory.Write<byte>(unitPointer - 0x94 * 2 + 0x7E, 0, false);

            //this.memory.Write<byte>((IntPtr)0x7BC58C - 0x94 * 1 + 0x7C, vehicleData3[0], false);
            //this.memory.Write<byte>((IntPtr)0x7BC58C - 0x94 * 1 + 0x7D, vehicleData3[1], false);
            //this.memory.Write<byte>((IntPtr)0x7BC58C - 0x94 * 1 + 0x7E, vehicleData3[2], false);

            //test

            //this.memory.Write<byte>(unitPointer + 0x1D, 36, false);
            //this.memory.Write<byte>(unitPointer - 0x94 + 0x1D, 36, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BCAC0, 1918, false);
            //this.readTest = this.memory.Read<byte>((IntPtr)0x797B70, 153294, false); //It works!
            //this.readTest = this.memory.Read<byte>((IntPtr)0x798870, 153294, false); // Also works!
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BBB20, 5918, false); // Works with copy unit
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BC870, 592, false); // Works with copy unit
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BC904, 444, false); // Works with copy unit
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BC998, 296, false); // Works with copy unit
            //this.readTest = this.memory.Read<byte>((IntPtr)0x7BCA2C, 148, false); // Not working
            this.readTest = this.memory.Read<byte>((IntPtr)0x5182C0, 1128, false); // works with copy unit
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int key = 0; key < this.readTest.Length; ++key)
            {
                this.memory.Write<byte>((IntPtr)0x5182C0 + key, this.readTest[key], false);
            }
        }
    }
}
