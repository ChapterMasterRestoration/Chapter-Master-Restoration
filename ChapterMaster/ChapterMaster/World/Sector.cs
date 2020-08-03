using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.World
{
    class Sector
    {
        //int seed = 31; // 41 is special!!!!
        public Random random;
        private int systemId;
        public List<System> Systems = new List<System>();
        // static organization of lane graph
        public List<Tuple<int, int>> WarpLanes; // like this?

        //int LocalSystem(int i)
        //{
        //    for (int localSystem = 0; localSystem < 4; localSystem++)
        //    {
        //        if(random.Next(7) > 5)
        //        {
        //            i++;
        //            i = LocalSystem(i);
        //            return i;
        //        }
        //        Systems.Add(new System(random.Next(7), x, y));
        //    }
        //    return 4;
        //}

        public void Prepare()
        {
            random = new Random();
        }
        int RoundedDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
        }
        int Clusters = 0;
        int lastClusterX = 0;
        int lastClusterY = 0;
        public void Generate()
        {
            int lastX = 0;
            int lastY = 0;
            for (int localSystem = 0; localSystem < 4; localSystem++)
            {
                if (random.Next(10) > 1)
                {
                    int x = lastX + random.Next(500) + 100;
                    int y = lastY + random.Next(300) + 100;
                    bool go = true;
                    foreach (System sys in Systems)
                    {
                        if (RoundedDistance(sys.x, sys.y, x, y) < 100) { go = false; }
                    }
                    if (go)
                    {
                        Systems.Add(new System(random.Next(6), x % 1280, y % 960));
                        lastX = x;
                        lastY = y;
                    }
                    continue;
                }
                else
                {
                    if (Clusters < 100)
                    {
                        //if (RoundedDistance(lastClusterX, lastClusterY, lastX, lastY) < 200) { return; }
                        Generate();
                        Clusters++;
                        lastClusterX = lastX;
                        lastClusterY = lastY;
                    }
                    else
                    {
                        if (Clusters > 50)
                        {
                            Debug.WriteLine("Reached Cluster Limit in Generation.");
                            return;
                        }
                    }
                }
            }
        }
        public void Generate(int number) // still doesn't work.
        {
            Systems.Add(new System(random.Next(6), random.Next(1280), random.Next(960)));
            while(Systems.Count < number)
            {
                foreach (System sys in Systems)
                {
                    int x = random.Next(1280);
                    int y = random.Next(960);
                    if (RoundedDistance(sys.x, sys.y, x, y) > 100) Systems.Add(new System(random.Next(6), x, y));
                    Generate(number);
                }
            }
        }
    }
}
