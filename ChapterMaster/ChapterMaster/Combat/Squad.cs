using ChapterMaster.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapterMaster.Combat.Order;

namespace ChapterMaster.Combat
{
    public class Squad
    {
        public string Name = "Maneus Squad";
        public Planet Planet;
        public string Faction = "Space Marine";
        public Vector2 Position = new Vector2(0,0);
        public List<Troop> Troops = new List<Troop>();
        //public Troop SquadLeader;
        public bool Grabbed;
        public bool Selected;
        public Vector2 troopOffset;
        public Vector2 startDragPosition;
        public OrderChain OrderChain = new OrderChain();
        public Squad(Planet Planet, List<Troop> Troops, string Faction)
        {
            this.Planet = Planet;
            this.Troops = Troops;
            this.Faction = Faction;
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

        public Troop GetSquadLeader()
        {
            return Troops[0];
        }

        public bool IsSquadLeader(Troop troop)
        {
            return troop == GetSquadLeader();
        }

        public Troop GetTroopUnderMouse(CombatViewController view)
        {
            foreach (Troop troop in Troops)
            {
                if (troop.GetRectangle(view, this).Contains(view.GetMouse().Position))
                {
                    //Debug.WriteLine("detected collision");
                    return troop;
                }
            }
            return null;
        }
        
    }
}
