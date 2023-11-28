using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune_Trainer.Common
{
    public class VehicleManager
    {
        private Dictionary<byte, string> types;

        public VehicleManager() {
            this.types = new Dictionary<byte, string>();
            this.types.Add(0, "Light Infantry");
            this.types.Add(1, "Trooper");
            this.types.Add(2, "Engineer");
            this.types.Add(3, "Thumper");
            this.types.Add(4, "Sardaukar");
            this.types.Add(5, "Trike");
            this.types.Add(6, "Raider");
            this.types.Add(7, "Quad");
            this.types.Add(8, "Harvester");
            this.types.Add(9, "Combat tank (A)");
            this.types.Add(10, "Combat tank (H)");
            this.types.Add(11, "Combat tank (O)");
            this.types.Add(12, "MCV");
            this.types.Add(13, "Missile Tank");
            this.types.Add(14, "Deviator");
            this.types.Add(15, "Siege tank");
            this.types.Add(16, "Sonic tank");
            this.types.Add(17, "Devastator");
            this.types.Add(18, "Carryall");
            this.types.Add(19, "Carryall (A)");
            this.types.Add(20, "Ornithropter");
            this.types.Add(21, "Stealth Fremen");
            this.types.Add(22, "Fremen");
            this.types.Add(23, "Saboteur");
            this.types.Add(24, "Death Hand Missile");
            this.types.Add(25, "Sandworm");
            this.types.Add(26, "Frigate");
            this.types.Add(27, "Grenadier");
            this.types.Add(28, "Stealth Raider");
            this.types.Add(29, "MP Sardaukar");
        }
        public Dictionary<byte, string> GetTypes()
        {
            return types;
        }
    }
}
