using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Prev { get; set; }
        public StackItem<T> Next { get; set; }        
    }

    public class LimitedSizeStack<T>
    {
        private int maxSize;
        private StackItem<T> head;
        private StackItem<T> tail;

        public LimitedSizeStack(int limit)
        {
            maxSize = limit;
            Count = 0;
        }

        public void Push(T item)
        {
            if (head == null)
                tail = head = new StackItem<T> { Value = item, Prev = null, Next = null };
            else
            {
                var newItem = new StackItem<T> { Value = item, Prev = tail, Next = null };
                tail.Next = newItem;
                tail = newItem;
            }

            Count++;

            if (Count > maxSize)
                DeleteFirstItem();
        }

        public T Pop()
        {
            if (head == null) throw new InvalidOperationException();
            var result = tail.Value;
            if (head==tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                tail = tail.Prev;
                tail.Next.Prev = null;
                tail.Next = null;
            }
            Count--;
            return result;
        }

        public int Count { get; private set; }

        private void DeleteFirstItem()
        {
            if (head == tail)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Prev.Next = null;
                head.Prev = null;
            }
            Count--;
        }
    }
}
