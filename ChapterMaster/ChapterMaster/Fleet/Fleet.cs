using ChapterMaster.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Fleet
{
    class Fleet
    {
        public int originSystemId;
        public int destinationSystemId;
        public int fleetSpeed = 50;
        public int fleetFaction;
        public int fleetMoveProgress;
        public bool isMoving;
        public int fleetState;
        public bool isSelected;
        public void Update(Sector sector)
        {
            if (fleetMoveProgress == sector.CalculateTravelTurns(this))
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
