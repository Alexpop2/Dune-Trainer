using Dune_Trainer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dune_Trainer
{
    public partial class Main : Form
    {
        private MemoryService memoryService;
        public Main(MemoryService memoryService)
        {
            InitializeComponent();
            this.memoryService = memoryService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(memoryService);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutoBuilder form = new AutoBuilder(memoryService);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuildingAvailability form = new BuildingAvailability(memoryService);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UnitListTest form = new UnitListTest(memoryService);
            form.Show();
        }
    }
}
