using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster
{
    public class System
    {
        public int color;
        public int x, y;
        public int numberOfLanes = 0;
        public System(int color, int x, int y) {
            this.color = color;
            this.x = x;
            this.y = y;
        }
        public void OpenPlanetScreen(ViewController view, int systemId)
        {
            if (!view.PlanetScreenOpen) {
                ChapterMaster.MainScreen.Screens.Add(new UI.PlanetScreen(1,"planetscreen",systemId));
                Debug.WriteLine("Added screen at x " + x + " y " + y);
            }
        }
        public void ClosePlanetScreen(ViewController view)
        {

        }
    }
}
