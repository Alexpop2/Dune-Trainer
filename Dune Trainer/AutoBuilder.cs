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
    public partial class AutoBuilder : Form
    {
        private IntPtr lastQueuePointer;
        private VehicleManager vehicleManager;
        private MemorySharp memory;
        private Dictionary<byte, Thread> autoBuilderThreads;
        private List<IntPtr> queueOffsets;
        public AutoBuilder(MemoryService memoryService)
        {
            InitializeComponent();

            this.queueOffsets = new List<IntPtr>();
            queueOffsets.Add((IntPtr)0x0);
            this.autoBuilderThreads = new Dictionary<byte, Thread>();
            this.memory = memoryService.GetMemory();
            this.vehicleManager = new VehicleManager();
            this.lastQueuePointer = (IntPtr)0x7BEE54;

            foreach(var vehicle in vehicleManager.GetTypes())
            {
                comboBox2.Items.Add(vehicle.Value);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.lastQueuePointer = (IntPtr)0x7BEE54;
                    break;
                case 1:
                    this.lastQueuePointer = (IntPtr)0x7E57E4;
                    break;
                case 2:
                    this.lastQueuePointer = (IntPtr)0x80C174;
                    break;
                case 3:
                    this.lastQueuePointer = (IntPtr)0x832B04;
                    break;
                case 4:
                    this.lastQueuePointer = (IntPtr)0x859494;
                    break;
                case 5:
                    this.lastQueuePointer = (IntPtr)0x87FE24;
                    break;
                case 6:
                    this.lastQueuePointer = (IntPtr)0x8A67B4;
                    break;
                case 7:
                    this.lastQueuePointer = (IntPtr)0x8CD144;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte selectedUnit = (byte)comboBox2.SelectedIndex;
            if (!this.autoBuilderThreads.ContainsKey(selectedUnit))
            {
                var thread = new Thread(() => autoBuilderThread(lastQueuePointer, selectedUnit));
                autoBuilderThreads.Add(selectedUnit, thread);
                thread.Start();
                UpdateAutoBuilderTable();
            }
        }
        private void UpdateAutoBuilderTable()
        {
            dataGridView1.Rows.Clear();

            foreach (byte key in autoBuilderThreads.Keys)
            {
                dataGridView1.Rows.Add(key, vehicleManager.GetTypes()[key]);
            }
        }

        private void autoBuilderThread(IntPtr lastQueuePointer, byte unitId)
        {
            while (true)
            {
                for (int i = 0; i < 10;  i++)
                {
                    //i = 9; // 16 units and stops??
                    var queueUnitId = this.memory.Read<byte>(lastQueuePointer - 0x14 * i, 1, false)[0];
                    if (queueUnitId == 255)
                    {
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x5, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x6, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x7, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x8, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x9, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0xA, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0xB, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i, unitId, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x1, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x2, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x3, 0, false);
                        this.memory.Write<byte>(lastQueuePointer - 0x14 * i + 0x4, 0, false);
                        this.memory.Write<int>(lastQueuePointer - 0x14 * i + 0xC, 0, false); 

                        var previousProgress = 0;
                        while(true)
                        {
                            var progressReadBytes = this.memory.Read<byte>(lastQueuePointer - 0x14 * i + 0x2, 2, false);
                            var progress = BitConverter.ToInt16(progressReadBytes, 0); ;

                            if (progress == 23040 || progress < previousProgress)
                            {
                                i = 10; 
                                break;
                            }
                            previousProgress = progress;
                        }
                    }
                }
                if (!this.autoBuilderThreads.ContainsKey(unitId))
                {
                    break;
                }
                Thread.Sleep(25);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var unitId = (byte)dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value;
                autoBuilderThreads.Remove(unitId);
                UpdateAutoBuilderTable();
            }
        }
    }
}
