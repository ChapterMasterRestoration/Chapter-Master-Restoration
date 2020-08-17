using ChapterMaster.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Fleet
{
    public class Fleet
    {
        /// <summary>
        /// might not be updated, only for mouse selection over multiple fleets!
        /// </summary>
        public int fleetId;
        public bool checkedByCoFleet;
        public List<int> coFleets = new List<int>();
        public int originSystemId;
        public int destinationSystemId;
        public int fleetSpeed = 50;
        public int fleetFaction;
        public int fleetMoveProgress;
        public bool isMoving;
        public int fleetState;
        public bool isSelected;
        public Fleet(int systemId, int fleetFaction, int fleetSate)
        {
            this.originSystemId = systemId;
            this.fleetFaction = fleetFaction;
            this.fleetState = fleetSate;
        }
        public void Update(Sector sector)
        {
            if (fleetMoveProgress == sector.CalculateTravelTurns(this)-1)
            {
                originSystemId = destinationSystemId;
                isMoving = false;
                fleetMoveProgress = 0;
            } else
            {
                fleetMoveProgress++;
            }
        }
    }
}
