using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Solutions
{
    [DebuggerDisplay("Data = {Data}")]
    public class LinkedListNode<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Prev { get; set; }
        public LinkedListNode<T> Last { get; set; }
        public T Data { get; set; }

        public LinkedListNode(T d, LinkedListNode<T> n, LinkedListNode<T> p)
        {
            Data = d;
            SetNext(n);
            SetPrevious(p);
        }

        public LinkedListNode()
        { }

        public void SetNext(LinkedListNode<T> n)
        {
            Next = n;
            if (this == Last)
            {
                Last = n;
            }
            if (n != null && n.Prev != this)
            {
                n.SetPrevious(this);
            }
        }

        public void SetPrevious(LinkedListNode<T> p)
        {
            Prev = p;
            if (p != null && p.Next != this)
            {
                p.SetNext(this);
            }
        }

        public String PrintForward()
        {
            if (Next != null)
            {
                return string.Format("{0}->{1}", Data, Next.PrintForward());
            }
            else
            {
                return string.Format("{0}", Data);
            }
        }

        public LinkedListNode<T> Clone()
        {
            LinkedListNode<T> next2 = null;
            if (Next != null)
            {
                next2 = Next.Clone();
            }
            LinkedListNode<T> head2 = new LinkedListNode<T>(Data, next2, null);
            return head2;
        }
    }
}
