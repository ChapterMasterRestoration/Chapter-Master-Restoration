using ChapterMaster.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    public class Tree // TODO: make generic or something
    {
        public Force Parent;

        public float CalculateWidth(int level, CalculateWidth calculateWidth, float offset)
        {
            float width = 0;
            if (level == 0) width = calculateWidth(Parent);
            for (int secondlevelnodes = 0; secondlevelnodes < Parent.GetNumberOfChildren(); secondlevelnodes++)
            {
                Force secondlevelnode = (Force) Parent.GetChildren()[secondlevelnodes];
                for (int thirdlevelnodes = 0; thirdlevelnodes < secondlevelnode.GetNumberOfChildren();thirdlevelnodes++)
                {
                    Force thirdlevelnode = (Force) secondlevelnode.GetChildren()[thirdlevelnodes];
                    for (int fourthlevelnodes = 0; fourthlevelnodes < thirdlevelnode.GetNumberOfChildren(); fourthlevelnodes++)
                    {

                    }
                    if (level == 2)
                        width += calculateWidth(thirdlevelnode) + offset;
                }
                if(level == 1)
                    width += calculateWidth(secondlevelnode) + offset;
            }
            return width;
        }
        //private int TraverseDepth(int prevDepth)
        //{
        //    foreach(Node node in Parent.GetChildren())
        //    {
        //        TraverseDepth()
        //    }
        //    return prevDepth;
        //}
        //public int CalculateDepth()
        //{

        //}
    }
}
