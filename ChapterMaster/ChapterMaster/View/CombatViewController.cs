using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChapterMaster.Combat;
using ChapterMaster.State;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ChapterMaster
{
    public class CombatViewController : ViewController
    {
        private GroundCombatState state;

        public CombatViewController(GroundCombatState state)
        {
            this.state = state;
        }
        public override void UpdateKeyboard()
        {
            base.UpdateKeyboard();
            if (Keyboard.GetState().IsKeyDown(Keys.Home))
            {
                camX = camY = 0;
            }
        }

        public override void UpdateMouse()
        {
            base.UpdateMouse();
        }

        public override void CheckBoundaries()
        {

        }

        // Troop stuff
        bool draggingTroop = false;
        private bool draggingSquad = false;
        public Troop currentlySelectedTroop;
        public Squad currentlySelectedSquad;
        Vector2 mouseOffset = new Vector2(0, 0);
        private Vector2 startDragPositionTroop;

        
        // Squad stuff
        //private Vector2 startDragPositionSquad;
        //Vector2 troopOffset = new Vector2(0, 0);
        // assigning orders
        public bool assigningOrder = false;
        public Vector2 orderStart = new Vector2(0, 0);
        public Vector2 orderEnd = new Vector2(0, 0);
        // selection
        List<Squad> selectedSquads = new List<Squad>();
        
        //private Squad currentSelSquad;

        public void MouseSelection(GroundCombatState groundCombatState)
        {
            if (IsDeploying())
            {
                for (int currentSquad = 0; currentSquad < groundCombatState.playerSquads.Count; currentSquad++)
                {
                    Squad squad = groundCombatState.playerSquads[currentSquad];
                    if (squad.Faction == ChapterMaster.Sector.CurrentFaction)
                    {
                        for (int currentTroop = 0; currentTroop < squad.Troops.Count; currentTroop++)
                        {
                            Troop troop = squad.Troops[currentTroop];
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                            {
                                if (draggingSquad)
                                {
                                    draggingSquad = false;
                                }

                                if (Mouse.GetState().RightButton == ButtonState.Pressed) // start drag
                                {
                                    if (!assigningOrder && currentlySelectedTroop == null &&
                                        currentlySelectedSquad == null && troop.MouseOver(this, squad))
                                    {
                                        mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                        troop.Grabbed = true;
                                        currentlySelectedTroop = troop;
                                        currentlySelectedSquad = squad;
                                        startDragPositionTroop = currentlySelectedTroop.Position;
                                        Debug.WriteLine("start drag position: " + startDragPositionTroop.ToString());
                                        draggingTroop = true;
                                    }
                                }
                                if (troop.Grabbed && currentlySelectedTroop == troop && currentlySelectedSquad == squad)
                                {
                                    if (draggingTroop)
                                    {
                                        Debug.WriteLine(
                                            $"Currently moving troop i {currentTroop} to ${Mouse.GetState().Position.X}");
                                        // Check for collisions
                                        //Vector2 prevPosition = currentlySelectedTroop.Position;
                                        currentlySelectedTroop.Position =
                                            Mouse.GetState().Position.ToVector2() -
                                            mouseOffset; // maybe check for collision here and find last valid position?
                                        if (Mouse.GetState().RightButton == ButtonState.Released) // release drag
                                        {
                                            foreach (Squad other in
                                                groundCombatState.playerSquads) // TODO: implement for rest of squads
                                            {
                                                Debug.WriteLine("over squad" + other.Troops.Count);
                                                if (currentlySelectedSquad != null && currentlySelectedTroop != null)
                                                {
                                                    if (currentlySelectedTroop.IsCollidingWithAny(this,
                                                        currentlySelectedSquad,
                                                        other))
                                                    {
                                                        Debug.WriteLine("collision squad: " + squad.Troops.Count +
                                                                        "with other squad: " + other.Troops.Count);
                                                        currentlySelectedTroop.Position = startDragPositionTroop;
                                                    }
                                                    else
                                                    {
                                                        Debug.WriteLine("no collision");
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.WriteLine("this should not happen");
                                                }
                                            }

                                            troop.Grabbed = false;
                                            Debug.WriteLine("drag released");
                                            currentlySelectedTroop = null;
                                            currentlySelectedSquad = null;
                                            draggingTroop = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                assigningOrder = false;
                                if (draggingTroop)
                                {
                                    if (Mouse.GetState().RightButton ==
                                        ButtonState.Released) // does this actually work as intended?
                                    {
                                        foreach (Squad other in
                                            groundCombatState.playerSquads) // TODO: implement for rest of squads
                                        {
                                            Debug.WriteLine("over squad" + other.Troops.Count);
                                            if (currentlySelectedSquad != null && currentlySelectedTroop != null)
                                            {
                                                if (currentlySelectedTroop.IsCollidingWithAny(this,
                                                    currentlySelectedSquad,
                                                    other))
                                                {
                                                    Debug.WriteLine("collision squad: " + squad.Troops.Count +
                                                                    "with other squad: " + other.Troops.Count);
                                                    currentlySelectedTroop.Position = startDragPositionTroop;
                                                }
                                                else
                                                {
                                                    Debug.WriteLine("no collision");
                                                }
                                            }
                                            else
                                            {
                                                Debug.WriteLine("this should not happen");
                                            }
                                        }
                                        Debug.WriteLine("drag released");
                                        if (!draggingSquad)
                                        {
                                            troop.Grabbed = false;
                                            currentlySelectedTroop = null;
                                            currentlySelectedSquad = null;
                                        }

                                        draggingTroop = false;
                                    }
                                }
                                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                                {
                                }

                                if (!draggingSquad && !draggingTroop && !assigningOrder) // TODO: fix this.
                                {
                                    currentlySelectedTroop = null;
                                    currentlySelectedSquad = null;
                                }
                            }
                            // assign order to squad

                            /*if (Mouse.GetState().LeftButton == ButtonState.Pressed) // start order
                            {
                                if (currentlySelectedTroop == null && troop.MouseOver(this, squad))
                                {
                                    mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                    troop.Grabbed = true;
                                    currentlySelectedTroop = troop;
                                    currentlySelectedSquad = squad;
                                    draggingTroop = false;
                                    orderStart = squad.Position + troop.Position +
                                                 new Vector2(troop.Size.X, troop.Size.Y / 2);
                                    assigningOrder = true;
                                }
                            }
                            if(!draggingTroop)
                            {
                                orderEnd = Mouse.GetState().Position.ToVector2();
                                currentlySelectedTroop.Rotation = GetOrderRotation();
                                Debug.WriteLine(orderEnd.ToString());
                                if (Mouse.GetState().LeftButton == ButtonState.Released) // release order
                                {
                                    troop.Grabbed = false;
                                    currentlySelectedTroop = null;
                                    assigningOrder = false;
                                }
                            }*/


                        }
                    }
                }

                // select squads
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    for (int currentSquad = 0; currentSquad < groundCombatState.playerSquads.Count; currentSquad++)
                    {
                        Squad squad = groundCombatState.playerSquads[currentSquad];
                        Troop troop = squad.GetTroopUnderMouse(this);
                        if (troop != null)
                        {
                            //Debug.WriteLine("started left click");
                            if (previousLMBState == ButtonState.Released) // pressed released with mouse over
                            {
                                // select squad
                                if (!selectedSquads.Contains(squad))
                                {
                                    Debug.WriteLine("added squad" + currentSquad);
                                    selectedSquads.Add(squad);
                                    squad.Selected = true;
                                    // initiate dragging squad
                                    if (!draggingSquad)
                                    {
                                        //troopOffset = GetMouse().Position.ToVector2() - selectedSquads[0].Position;
                                        squad.troopOffset = GetMouse().Position.ToVector2() - squad.Position;
                                        squad.startDragPosition = squad.Position; // todo: implement
                                        draggingSquad = true;
                                    }
                                }
                                else
                                {
                                    if (!draggingSquad)
                                    {
                                        Debug.WriteLine("removed squad" + currentSquad);
                                        squad.Selected = false;
                                        selectedSquads.Remove(squad);
                                    }
                                }
                            }
                        }

                    }

                    if (!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                    {
                        if (previousLMBState == ButtonState.Pressed) // pressed pressed
                        {
                            // move squad
                            if (draggingSquad && selectedSquads.Count > 0)
                            {
                                bool wasOverOtherTroop = false;
                                foreach (Squad s in selectedSquads)
                                {
                                    foreach (Squad otherSquad in selectedSquads)
                                    {
                                        if (!selectedSquads.Contains(otherSquad))
                                        {
                                            if (otherSquad.GetTroopUnderMouse(this) != null) wasOverOtherTroop = true;
                                        }
                                    }
                                    if(!wasOverOtherTroop && s.GetTroopUnderMouse(this) != null)
                                        s.Position = Mouse.GetState().Position.ToVector2() - s.troopOffset;
                                }

                                //Debug.WriteLine($"Currently moving squad i {0} to ${Mouse.GetState().Position.X}");
                                //troop.Grabbed = true;
                            }
                        }
                        else if (previousLMBState == ButtonState.Released && !draggingSquad) // pressed released
                        {
                            for (int squadToDeselect = 0;
                                squadToDeselect < selectedSquads.Count;
                                squadToDeselect++)
                            {
                                selectedSquads[squadToDeselect].Selected = false;
                                selectedSquads.RemoveAt(squadToDeselect);
                                Debug.WriteLine("deselecting all");
                            }
                        }
                    }
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released && draggingSquad && selectedSquads.Count > 0 && !Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                {
                    if (previousLMBState == ButtonState.Pressed)
                    {
                        Debug.WriteLine("lifted move");
                        foreach (Squad selectedSquad in selectedSquads)
                        {
                            foreach (Squad other in groundCombatState.playerSquads) // TODO: Implement for rest of squads.
                            {
                                //Debug.WriteLine("over squad" + other.Troops.Count);
                                foreach (Troop t in selectedSquad.Troops)
                                {
                                    if (t.IsCollidingWithAny(this, selectedSquad, other))
                                    {

                                        selectedSquad.Position =
                                            selectedSquad.startDragPosition; //- selectedSquad.troopOffset;
                                    }
                                }
                            }
                        }

                        draggingSquad = false;
                    }
                }

                previousLMBState = GetMouse().LeftButton;
            }
        }
        
        
        
        /*
                                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                                {
                                    if (!draggingSquad && currentlySelectedSquad == null &&
                                        currentlySelectedTroop == null &&
                                        troop.MouseOver(this, squad))
                                    {
                                        //mouseOffset = Mouse.GetState().Position.ToVector2() - troop.Position;
                                        troopOffset = Mouse.GetState().Position.ToVector2() - squad.Position;
                                        startDragPositionSquad = Mouse.GetState().Position.ToVector2();
                                        troop.Grabbed = true;
                                        squad.Grabbed = true;
                                        currentlySelectedTroop = troop;
                                        currentlySelectedSquad = squad;
                                        draggingSquad = true;
                                    }
                                }
                            }
                            */
        
        
        
                                /*if (!assigningOrder && draggingSquad && troop.Grabbed && squad.Grabbed &&
                                   currentlySelectedTroop == troop &&
                                   currentlySelectedSquad == squad) // release whole squad
                               {
                                   squad.Position = Mouse.GetState().Position.ToVector2() - troopOffset;
    
                                   if (Mouse.GetState().RightButton == ButtonState.Released)
                                   {
                                       foreach (Squad other in
                                           groundCombatState.playerSquads) // TODO: Implement for rest of squads.
                                       {
                                           Debug.WriteLine("over squad" + other.Troops.Count);
                                           if (currentlySelectedSquad != null && currentlySelectedTroop != null)
                                           {
                                               foreach (Troop t in currentlySelectedSquad.Troops)
                                               {
                                                   if (t.IsCollidingWithAny(this, currentlySelectedSquad, other))
                                                   {
    
                                                       currentlySelectedSquad.Position =
                                                           startDragPositionSquad - troopOffset;
                                                   }
                                               }
    
                                           }
                                       }
    
                                       troop.Grabbed = false;
                                       squad.Grabbed = false;
                                       currentlySelectedTroop = null;
                                       currentlySelectedSquad = null;
                                       draggingSquad = false;
                                   }*/
        public Vector2 GetStartPositionOfOrder()
        {
            return orderStart + new Vector2(0, 0);
        }

        public float GetOrderRotation()
        {
            return (float) Math.Atan2(orderEnd.Y - orderStart.Y, orderEnd.X - orderStart.X);

        }
        public float GetOrderScale()
        {
            return (orderEnd - orderStart).Length()/128;
        }

        public Vector2 GetCameraPosition()
        {
            return new Vector2(camX, camY);
        }

        public bool IsDeploying()
        {
            return state.IsDeploying;
        }
     }
}