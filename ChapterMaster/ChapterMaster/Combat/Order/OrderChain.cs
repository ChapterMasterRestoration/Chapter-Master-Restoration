namespace ChapterMaster.Combat.Order
{
    public class OrderChain
    {
        private Order Start;

        public void AddOrderLast(Order order) // deal with deleting previous order
        {
            if (Start == null)
            {
                Start = new Order();
                Start.Finished = true;
            }
            else
            {
                Order current = Start;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = order;
            }
        }
        public Order GetCurrentOrder()
        {
            if (Start != null)
            {
                Order current = Start;
                while (current.Next != null)
                {
                    current = current.Next;
                    if (!current.Finished)
                    {
                        return current;
                    }
                }
                
            }

            return null;
        }
    }
}