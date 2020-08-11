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
        public int fleetSpeed;
        public int fleetFaction;
        public int fleetMoveProgress;
        public bool isMoving;
        public int fleetState;

        public void Update()
        {
            if (fleetMoveProgress == fleetSpeed)
            {
                originSystemId = destinationSystemId;
                isMoving = false;
                fleetMoveProgress = 0;
            }
            
        }
        





    }
}
