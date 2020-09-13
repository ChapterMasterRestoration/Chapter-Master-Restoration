using ChapterMaster.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //public bool wasJustSelected;
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
        public bool Intersects(ViewController view)
        {
            List<Fleet> orbitingFleets = new List<Fleet>();
            for (int oFleetId = 0; oFleetId < GameState.sector.Fleets.Count; oFleetId++)
            {
                GameState.sector.Fleets[oFleetId].fleetId = oFleetId; // TODO: will this create problems when the list of fleets changes?
                if (GameState.sector.Fleets[oFleetId].originSystemId == originSystemId)
                {
                    orbitingFleets.Add(GameState.sector.Fleets[oFleetId]);
                    if (!GameState.sector.Fleets[fleetId].coFleets.Contains(oFleetId))
                    {  
                        if (oFleetId != fleetId)
                            GameState.sector.Fleets[fleetId].coFleets.Add(oFleetId);
                    }
                }
            }
            for (int orbitingFleetId = 0; orbitingFleetId < orbitingFleets.Count; orbitingFleetId++)
            {
                if (orbitingFleets[orbitingFleetId].coFleets.Contains(fleetId))
                {
                    
                }
                int ulCornerX = (int)((GameState.sector.Systems[originSystemId].x + (Constants.SystemSize / 4) + 30 - view.camX) * view.zoom + GameState.GetWidth() / 2);
                int ulCornerY = (int)((GameState.sector.Systems[originSystemId].y + (Constants.SystemSize / 4) - 30 - view.camY) * view.zoom + GameState.GetHeight() / 2);
                int brCornerX = (int)((GameState.sector.Systems[originSystemId].x + (Constants.SystemSize / 4) + 30 + Constants.SystemSize / 2 - view.camX) * view.zoom + GameState.GetWidth() / 2);
                int brCornerY = (int)((GameState.sector.Systems[originSystemId].y + (Constants.SystemSize / 4) - 30 + Constants.SystemSize / 2 - view.camY) * view.zoom + GameState.GetHeight() / 2);
                int fleetWidth = brCornerX - ulCornerX;
                ulCornerX = ulCornerX + fleetWidth * orbitingFleetId;
                brCornerX = brCornerX + fleetWidth * orbitingFleetId;
                if (view.GetMouse().X > ulCornerX && view.GetMouse().Y > ulCornerY && view.GetMouse().X < brCornerX && view.GetMouse().Y < brCornerY)
                {
                    Debug.WriteLine($"  Orbiting Fleet ID {orbitingFleetId} Filet ID: {fleetId}");
                    //if (!orbitingFleets[orbitingFleetId].checkedByCoFleet)
                    //{
                    if (orbitingFleets[orbitingFleetId].fleetId == fleetId && !checkedByCoFleet)
                    {
                        //if (fleetId == 3) { Debug.WriteLine($"fleet id {fleetId}"); }
                        Debug.WriteLine($"fleet intersection in {orbitingFleets[orbitingFleetId].fleetId} by {fleetId} Check: {checkedByCoFleet} oCheck {orbitingFleets[orbitingFleetId].checkedByCoFleet}");
                        return true;
                    }
                    else
                    {
                        checkedByCoFleet = true;
                    }
                    //}
                }
                else
                {
                    checkedByCoFleet = false;
                }
            }
            return false;
        }
        public List<int> GetCofleetsMovingAlong()
        {
            List<int> fleets = new List<int>();
            for(int id = 0; id < coFleets.Count; id ++)
            {
                if(GameState.sector.Fleets[coFleets[id]].destinationSystemId == destinationSystemId && GameState.sector.Fleets[coFleets[id]].isMoving)
                {
                    fleets.Add(id);
                }
            }
            return fleets;
        }
    }
}
