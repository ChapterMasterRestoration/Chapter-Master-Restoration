﻿using ChapterMaster.World;
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
        public bool Intersects(ViewController view)
        {
            List<Fleet> orbitingFleets = new List<Fleet>();
            for (int oFleetId = 0; oFleetId < ChapterMaster.sector.Fleets.Count; oFleetId++)
            {
                if (ChapterMaster.sector.Fleets[oFleetId].originSystemId == originSystemId)
                {
                    if (!ChapterMaster.sector.Fleets[fleetId].coFleets.Contains(oFleetId))
                    {
                        ChapterMaster.sector.Fleets[oFleetId].fleetId = oFleetId; // TODO: will this create problems when the list of fleets changes?
                        orbitingFleets.Add(ChapterMaster.sector.Fleets[oFleetId]);
                        if (oFleetId != fleetId)
                            ChapterMaster.sector.Fleets[fleetId].coFleets.Add(oFleetId);
                    }
                }
                for (int orbitingFleetId = 0; orbitingFleetId < orbitingFleets.Count; orbitingFleetId++)
                {
                    if (orbitingFleets[orbitingFleetId].coFleets.Contains(fleetId))
                    {
                        orbitingFleets[orbitingFleetId].checkedByCoFleet = true;
                    }
                    int ulCornerX = (int)((ChapterMaster.sector.Systems[originSystemId].x + (Constants.SystemSize / 4) + 30 - view.camX) * view.zoom + ChapterMaster.GetWidth() / 2);
                    int ulCornerY = (int)((ChapterMaster.sector.Systems[originSystemId].y + (Constants.SystemSize / 4) - 30 - view.camY) * view.zoom + ChapterMaster.GetHeight() / 2);
                    int brCornerX = (int)((ChapterMaster.sector.Systems[originSystemId].x + (Constants.SystemSize / 4) + 30 + Constants.SystemSize / 2 - view.camX) * view.zoom + ChapterMaster.GetWidth() / 2);
                    int brCornerY = (int)((ChapterMaster.sector.Systems[originSystemId].y + (Constants.SystemSize / 4) - 30 + Constants.SystemSize / 2 - view.camY) * view.zoom + ChapterMaster.GetHeight() / 2);
                    int fleetWidth = brCornerX - ulCornerX;
                    ulCornerX = ulCornerX + fleetWidth * orbitingFleetId;
                    brCornerX = brCornerX + fleetWidth * orbitingFleetId;
                    if (view.GetMouse().X > ulCornerX && view.GetMouse().Y > ulCornerY && view.GetMouse().X < brCornerX && view.GetMouse().Y < brCornerY && !orbitingFleets[orbitingFleetId].checkedByCoFleet)
                    {
                        if (!orbitingFleets[orbitingFleetId].checkedByCoFleet)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
