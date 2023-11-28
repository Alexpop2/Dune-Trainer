using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune_Trainer.Services
{
    public class MemoryService
    {
        private readonly Process dune;
        private readonly MemorySharp memory;

        public MemoryService() 
        {
            this.dune = ApplicationFinder.FromProcessName("dune2000").First();
            this.memory = new MemorySharp(this.dune);
        }

        public MemorySharp GetMemory()
        {
            return this.memory;
        }
    }
}
