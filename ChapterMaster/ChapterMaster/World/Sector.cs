using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChapterMaster.Util;
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
        public List<WarpLane> WarpLanes = new List<WarpLane>(); // like this?

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
            random = new Random(41); //220
        }
        int RoundedDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
        }
        /*
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
            //Systems.Add(new System(random.Next(6), random.Next(1280), random.Next(960)));
            while (Systems.Count < number)
            {
                foreach (System sys in Systems)
                {
                    int x = random.Next(1280);
                    int y = random.Next(960);
                    if (RoundedDistance(sys.x, sys.y, x, y) > 100) Systems.Add(new System(random.Next(6), x, y));
                    //Generate(number);
                }
            }
        }


        int min = 30;
        int max = 100;
        public void NormalDistGenerate()
        {
            int count = (int)Math.Round(random.NormallyDistributedSingle(15, 60, min, max));
            int sprawl = 500;
            int average = 150;
            int lastX = 500;
            int lastY = 500;

            for (int i = 0; i < count; i++)
            {
                int x = (int)Math.Round(random.NormallyDistributedSingle(sprawl, average, -500, 500));
                int y = (int)Math.Round(random.NormallyDistributedSingle(sprawl, average, -300, 300)); ;
                int xBorder = 1280 - x;
                int yBorder = 960 - x;
                if (xBorder >= 0 && xBorder < 300 && random.Next(2) == 1)
                {
                    x = 1280 - x;
                }
                if (yBorder >= 0 && yBorder < 200 && random.Next(2) == 1)
                {
                    y = 960 - y;
                }
                Systems.Add(new System(random.Next(6),
                            MathUtil.ClampInt(lastX + x, 0, 1280),
                            MathUtil.ClampInt(lastY + y, 0, 960)));
                lastX = MathUtil.ClampInt(lastX + x, 0, 1280);
                lastY = MathUtil.ClampInt(lastY + y, 0, 960);
            }
            //lastX = lastY = 0;
        }
        */
        public void GridGenerate(int clusterNo, int minDistance, int clusterSize, int width, int height)
        {
            Systems.Clear();
            for (int x = 0; x < width / clusterSize; x++)
            {
                for (int y = 0; y < height / clusterSize; y++)
                {
                    int no = Systems.Count;
                    if (no < clusterNo)
                    {
                        int newX = x * clusterSize + (int)Math.Round(
                            random.NormallyDistributedSingle(width/2, width/4, -width/2, width/2)); // skew to the east
                        int newY = y * clusterSize + (int)Math.Round(
                            random.NormallyDistributedSingle(height/2, height/4, -height/2, height/2)); // skew to the south
                        if (no > 0)
                        {
                            for (int secondCircle = 0; secondCircle < no; secondCircle++) // hell loop
                            {
                                foreach (System s in Systems)
                                {
                                    int dis = RoundedDistance(s.x, s.y, newX, newY);
                                    if (dis < minDistance) // skew away from other stars
                                    {
                                        newX += (int)Math.Round(random.NormallyDistributedSingle(dis, 0, -2 * dis, 2 * dis)); 
                                        newY += (int)Math.Round(random.NormallyDistributedSingle(dis, 0, -2 * dis, 2 * dis));
                                    }
                                }
                            }
                        } 
                        if (newX < 0) newX += width; // randomize the new posiiton
                        if (newY <0) newY += height; // randomize the new position
                        if (random.Next(2) == 1 || newX > width) // sprawl out to the west
                        {
                            newX = width - newX;
                        }
                        if (random.Next(2) == 1 || newY > height) // sprawl out to the north
                        {
                            newY = height - newY;
                        }
                        Systems.Add(new System(random.Next(6), newX, newY));
                    }
                }
            }
        }
        public void WarpLaneGenerate()
        {
            WarpLanes.Clear();
            for(int system = 0; system < Systems.Count; system++)
            {
                for (int other = 0; other < Systems.Count; other++)
                {
                    if (system == other) // system is other
                    {
                        // ignore
                        continue;
                    }
                    else
                    {
                        int distance = RoundedDistance(
                            Systems[system].x, Systems[system].y, 
                            Systems[other].x, Systems[other].y);
                        if (Systems[system].numberOfLanes < 3 && Systems[other].numberOfLanes < 3)
                        {
                            // balance random factors
                            if (distance < 100 && random.Next(20) > 7)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 200 && random.Next(100) < 5)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 300 && random.Next(200) < 7)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 400 && random.Next(200) < 5)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                        }
                    }
                }
            }
        }
    }
}
