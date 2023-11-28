using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dune_Trainer.Common
{
    public class BuildingManager
    {
        private Dictionary<byte, string> types;

        public BuildingManager() {
            this.types = new Dictionary<byte, string>();
            this.types.Add(0, "Construction Yard (A)");
            this.types.Add(1, "Construction Yard (H)");
            this.types.Add(2, "Construction Yard (O)");
            this.types.Add(3, "Construction Yard (E)");
            this.types.Add(4, "Concrete (A)");
            this.types.Add(5, "Concrete (H)");
            this.types.Add(6, "Concrete (O)");
            this.types.Add(7, "Large Concrete (A)");
            this.types.Add(8, "Large Concrete (H)");
            this.types.Add(9, "Large Concrete (O)");
            this.types.Add(10, "Wind Trap (A)");
            this.types.Add(11, "Wind Trap (H)");
            this.types.Add(12, "Wind Trap (O)");
            this.types.Add(13, "Barracks (A)");
            this.types.Add(14, "Barracks (H)");
            this.types.Add(15, "Barracks (O)");
            this.types.Add(16, "Sietch");
            this.types.Add(17, "Wall (A)");
            this.types.Add(18, "Wall (H)");
            this.types.Add(19, "Wall (O)");
            this.types.Add(20, "Refinery (A)");
            this.types.Add(21, "Refinery (H)");
            this.types.Add(22, "Refinery (O)");
            this.types.Add(23, "Gun Turret (A)");
            this.types.Add(24, "Gun Turret (H)");
            this.types.Add(25, "Gun Turret (O)");
            this.types.Add(26, "Outpost (A)");
            this.types.Add(27, "Outpost (H)");
            this.types.Add(28, "Outpost (O)");
            this.types.Add(29, "Rocket Turret (A)");
            this.types.Add(30, "Rocket Turret (H)");
            this.types.Add(31, "Rocket Turret (O)");
            this.types.Add(32, "High Tech Factory (A)");
            this.types.Add(33, "High Tech Factory (H)");
            this.types.Add(34, "High Tech Factory (O)");
            this.types.Add(35, "Light Factory (A)");
            this.types.Add(36, "Light Factory (H)");
            this.types.Add(37, "Light Factory (O)");
            this.types.Add(38, "Silo (A)");
            this.types.Add(39, "Silo (H)");
            this.types.Add(40, "Silo (O)");
            this.types.Add(41, "Heavy Factory (A)");
            this.types.Add(42, "Heavy Factory (H)");
            this.types.Add(43, "Heavy Factory (O)");
            this.types.Add(44, "Heavy Factory (M)");
            this.types.Add(45, "Heavy Factory (E)");
            this.types.Add(46, "Starport (A)");
            this.types.Add(47, "Starport (H)");
            this.types.Add(48, "Starport (O)");
            this.types.Add(49, "Starport (S)");
            this.types.Add(50, "Repair Pad (A)");
            this.types.Add(51, "Repair Pad (H)");
            this.types.Add(52, "Repair Pad (O)");
            this.types.Add(53, "IX Research Centre (A)");
            this.types.Add(54, "IX Research Centre (H)");
            this.types.Add(55, "IX Research Centre (O)");
            this.types.Add(56, "Palace (A)");
            this.types.Add(57, "Palace (H)");
            this.types.Add(58, "Palace (O)");
            this.types.Add(59, "Palace (E)");
            this.types.Add(60, "Modified Outpost (H)");
            this.types.Add(61, "Modified Outpost (O)");
        }
        public Dictionary<byte, string> GetTypes()
        {
            return types;
        }
    }
}
