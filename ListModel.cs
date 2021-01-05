using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public interface ICommand<T>
    {
        void Do(List<T> items);
        void Undo(List<T> items);
    }

    public class CommandAdd<T>: ICommand<T>
    {
        private T item;

        public CommandAdd(T item)
        {
            this.item = item;
        }

        public void Do(List<T> items)
        {
            items.Add(item);
        }

        public void Undo(List<T> items)
        {
            items.Remove(item);
        }
    }

    public class CommandRemove<T> : ICommand<T>
    {
        private int index;
        private T item;

        public CommandRemove(int index)
        {
            this.index = index;
        }

        public void Do(List<T> items)
        {
            item = items[index];
            items.RemoveAt(index);
        }

        public void Undo(List<T> items)
        {
            items.Insert(index, item);
        }
    }

    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        public LimitedSizeStack<ICommand<TItem>> Commands { get; }

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            Commands = new LimitedSizeStack<ICommand<TItem>>(Limit);
        }

        public void AddItem(TItem item)
        {
            //Items.Add(item);
            ICommand<TItem> command = new CommandAdd<TItem>(item);
            Commands.Push(command);
            command.Do(Items);
        }

        public void RemoveItem(int index)
        {
            //Items.RemoveAt(index);
            ICommand<TItem> command = new CommandRemove<TItem>(index);
            Commands.Push(command);
            command.Do(Items);
        }

        public bool CanUndo()
        {
            return Commands.Count!=0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                ICommand<TItem> command = Commands.Pop();
                command.Undo(Items);
            }
        }
    }
}
