using ChapterMaster.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Combat
{
    public class Squad
    {
        public string Name = "Maneus Squad";
        public Planet Planet;
        public Vector2 Position = new Vector2(0,0);
        public List<Troop> Troops = new List<Troop>();
        public bool Grabbed;

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
