using Microsoft.Xna.Framework;

namespace ChapterMaster.Combat.Order
{
    public class MoveOrder : Order
    {
        private Vector2 start = new Vector2(0, 0);
        private Vector2 end = new Vector2(0, 0);

        public Vector2 Start { get => start; set => start = value; }
        public Vector2 End { get => end; set => end = value; }

        public MoveOrder(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}