using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutions;
using System;

namespace Tests
{
    [TestClass]
    public class Test_LinkedList
    {
        private readonly LinkedList linkedList;

        public Test_LinkedList()
        {
            linkedList = new LinkedList();
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

            this.linkedList.Q1_RemoveDuplicates<char>(head);

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
            Random rand = new Random();

            int count = rand.Next(1, 100);
            var sample = CreateLinkedList(count);

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
            Random rand = new Random();

            int count = rand.Next(1, 100);
            var sample = CreateLinkedList(count);

            int n = rand.Next(1, count);

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

        [TestMethod]
        public void Q2_4()
        {
            const int max = 30;

            //테스트 이전 설정
            Random rand = new Random();
            var head = new LinkedListNode<int>(rand.Next(0, max), null, null);

            LinkedListNode<int> prev = head, node;
            for (int i = 1; i < max; i++)
            {
                node = new LinkedListNode<int>(rand.Next(0, max), null, null);
                node.Prev = prev;
                prev.Next = node;

                prev = node;
            }

            //코드 테스트
            int pivot = rand.Next(0, max);
            var result = this.linkedList.Q4_Partition(head, pivot);

            //코드 테스트 결과 검증
            bool pass = false;
            var current = result;
            while (current != null)
            {
                if (pass)
                {
                    Assert.IsTrue(current.Data >= pivot);
                }
                else
                {
                    if (current.Data > pivot)
                    {
                        pass = true;
                    }
                }
                    
                current = current.Next;
            }
        }

        [TestMethod]
        public void Q2_5()
        {
            Random rand = new Random();
            var number1A = new LinkedListNode<int>(rand.Next(0, 10), null, null);
            var number1B = new LinkedListNode<int>(rand.Next(0, 10), null, number1A);
            var number1C = new LinkedListNode<int>(rand.Next(0, 10), null, number1B);

            var number2A = new LinkedListNode<int>(rand.Next(0, 10), null, null);
            var number2B = new LinkedListNode<int>(rand.Next(0, 10), null, number2A);
            var number2C = new LinkedListNode<int>(rand.Next(0, 10), null, number2B);

            var result = this.linkedList.Q5_AddNumbers(number1A, number2A, false);

            var cur = result;

            int ideal = (number1A.Data + number2A.Data) % 10;
            Assert.AreEqual(cur.Data, ideal);
            cur = cur.Next;
            ideal = (number1B.Data + number2B.Data) % 10 + (ideal >= 10 ? 1 : 0);
            Assert.AreEqual(cur.Data, ideal);
            cur = cur.Next;
            ideal = (number1C.Data + number2C.Data) % 10 + (ideal >= 10 ? 1 : 0);
            Assert.AreEqual(cur.Data, ideal);
        }

        [TestMethod]
        public void Q2_6()
        {
            Random rand = new Random();
            int listLength = rand.Next(2, 30);
            int k = rand.Next(1, listLength);

            // Create linked list
            var nodes = new LinkedListNode<int>[listLength];

            for (var i = 1; i <= listLength; i++)
            {
                nodes[i - 1] = new LinkedListNode<int>(i, null, i - 1 > 0 ? nodes[i - 2] : null);
                Console.Write("{0} -> ", nodes[i - 1].Data);
            }
            Console.WriteLine();

            // Create loop;
            nodes[listLength - 1].Next = nodes[listLength - k - 1];
            Console.WriteLine("{0} -> {1}", nodes[listLength - 1].Data, nodes[listLength - k - 1].Data);

            var loop = this.linkedList.Q6_FindBeginning<int>(nodes[0]);

            Assert.IsNotNull(loop);
        }
        [TestMethod]
        public void Q2_7()
        {
            Random rand = new Random();
            int length = rand.Next(2, 30);
            var nodes = new LinkedListNode<int>[length];

            for (var i = 0; i < length; i++)
            {
                nodes[i] = new LinkedListNode<int>(i >= length / 2 ? length - i - 1 : i, null, null);
            }

            for (var i = 0; i < length; i++)
            {
                if (i < length - 1)
                {
                    nodes[i].SetNext(nodes[i + 1]);
                }

                if (i > 0)
                {
                    nodes[i].SetPrevious(nodes[i - 1]);
                }
            }

            var head = nodes[0];
            
            Assert.IsTrue(this.linkedList.Q7_IsPalindrome(head));

            nodes[length - 2].Data = 9;

            Assert.IsFalse(this.linkedList.Q7_IsPalindrome(head));
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
