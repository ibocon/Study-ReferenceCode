using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System;

namespace Tests
{
    [TestClass]
    public class Test_LinkedList
    {
        private readonly Solutions.LinkedList linkedList;

        public Test_LinkedList()
        {
            linkedList = new Solutions.LinkedList();
        }

        [TestMethod]
        public void Q2_1()
        {
            Random rand = new Random();
            LinkedListNode<char> head = new LinkedListNode<char>(Convert.ToChar(rand.Next(65, 91)), null, null);
            LinkedListNode<char> prev = head;
            for (int index = 0; index < 10; index++)
            {
                var next = new LinkedListNode<char>(Convert.ToChar(rand.Next(65, 91)), null, prev);
                prev.SetNext(next);
                prev = next;
            }

            linkedList.Q1_RemoveDuplicates<char>(head);

            var a = head;
            while (a.Next != null)
            {
                var b = a;
                while (b.Next != null)
                {
                    b = b.Next;
                    Assert.AreNotEqual(a.Data, b.Data);
                }
                a = a.Next;
            }
        }

        [TestMethod]
        public void Q2_2()
        {
            var n1 = new LinkedListNode<char>('A', null, null);
            var n2 = new LinkedListNode<char>('B', null, n1);
            n1.SetNext(n2);
            var n3 = new LinkedListNode<char>('C', null, n2);
            n2.SetNext(n3);
            var n4 = new LinkedListNode<char>('D', null, n3);
            n3.SetNext(n4);
            var n5 = new LinkedListNode<char>('E', null, n4);
            n4.SetNext(n5);
            var n6 = new LinkedListNode<char>('F', null, n5);
            n5.SetNext(n6);
            var n7 = new LinkedListNode<char>('G', null, n6);
            n6.SetNext(n7);
            var n8 = new LinkedListNode<char>('H', null, n7);
            n7.SetNext(n8);

            Random rand = new Random();
            int n = rand.Next(1, 9);

            var result = linkedList.Q2_GetNthNodeFromLast<char>(n1, n);

            var expected = n8;
            for(int index = 1; index < n; index++)
            {
                expected = expected.Prev;
            }

            Assert.AreEqual(result.Data, expected.Data);
        }
    }
}
