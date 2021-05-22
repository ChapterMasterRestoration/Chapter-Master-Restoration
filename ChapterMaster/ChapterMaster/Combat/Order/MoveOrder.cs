using Microsoft.Xna.Framework;

namespace ChapterMaster.Combat.Order
{
    public class MoveOrder : Order
    {
        public Vector2 Start = new Vector2(0, 0);
        public Vector2 End = new Vector2(0, 0);

        public MoveOrder(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}