using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Native;
using Dune_Trainer.Common;
using Dune_Trainer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dune_Trainer
{
    public partial class BuildingAvailability : Form
    {
        private readonly BuildingManager buildingManager;
        private readonly MemorySharp memory;
        private IntPtr buildingPointer;
        public BuildingAvailability(MemoryService memoryService)
        {
            InitializeComponent();

            this.buildingManager = new BuildingManager();
            this.buildingPointer = (IntPtr)0x7B452C;

            this.memory = memoryService.GetMemory();

            foreach (var building in buildingManager.GetTypes())
            {
                dataGridView1.Rows.Add(building.Value);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
