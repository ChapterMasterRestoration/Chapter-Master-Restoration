using ChapterMaster.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    public class Squad
    {
        public string Name = "Bellarius Squad";
        public Planet Planet;
        public List<Troop> Troops = new List<Troop>();

        public Squad(Planet planet, List<Troop> troops)
        {
            this.Planet = planet;
            this.Troops = troops;
        }
        public float GetHealth()
        {
            float totalHealth = 0;
            foreach (Troop troop in Troops)
            {
                totalHealth += troop.Health;
            }
            return totalHealth;
        }
    }
}
