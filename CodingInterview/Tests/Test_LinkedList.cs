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
            int count = 8;
            var sample = CreateLinkedList(8);

            Random rand = new Random();
            int n = rand.Next(1, count + 1);

            var result = linkedList.Q2_GetNthNodeFromLast<char>(sample, n);

            LinkedListNode<char> expected = sample;
            while (expected.Next != null)
            {
                expected = expected.Next;
            }

            for(int index = 1; index < n; index++)
            {
                expected = expected.Prev;
            }

            Assert.AreEqual(result.Data, expected.Data);
        }

        [TestMethod]
        public void Q2_3()
        {
            int count = 8;
            var sample = CreateLinkedList(8);

            Random rand = new Random();
            int n = rand.Next(1, count+1);

            LinkedListNode<char> node = sample;
            for (int i = 0; i < n; i++)
            {
                node = node.Next;
            }

            Assert.IsTrue(linkedList.Q3_DeleteNode<char>(node));

            LinkedListNode<char> check = sample;
            for (int i = 1; i < count; i++)
            {
                Assert.AreNotEqual(node, check);
                check = check.Next;
            }
        }

        private LinkedListNode<char> CreateLinkedList(int count)
        {
            var head = new LinkedListNode<char>('A', null, null);

            LinkedListNode<char> prev = head, node;
            for(int i = 1; i < count; i++)
            {
                node = new LinkedListNode<char>((char)(Convert.ToUInt16('A') + i), null, prev);
                prev.SetNext(node);
                prev = node;
            }

            return head;
        }
    }
}
