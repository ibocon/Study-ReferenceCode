
using System;

namespace Solutions
{
    public class LinkedList
    {
        /// <summary>
        /// Question 2.1
        /// 정렬되지 않은 연결 리스트에서 중복 문자를 제거하라.
        /// (임시 버퍼가 허용되지 않는 상황)
        /// </summary>
        /// <typeparam name="T"><code>IComparable</code></typeparam>
        /// <param name="head">리스트의 시작지점</param>
        public void Q1_RemoveDuplicates<T>(LinkedListNode<T> head) where T : IComparable<T>
        {
            if (head == null) return;

            LinkedListNode<T> current = head;

            while (current != null)
            {
                LinkedListNode<T> runner = current;

                while (runner.Next != null)
                {
                    if (current.Data.CompareTo(runner.Next.Data) == 0)
                    {
                        runner.Next = runner.Next.Next;

                        if (runner.Next != null)
                        {
                            runner.Next.Prev = runner;
                        }
                    }
                    else
                    {
                        runner = runner.Next;
                    }
                }

                current = current.Next;
            }

        }

        /// <summary>
        /// Question 2.2
        /// 뒤에서 <paramref name="n">n</paramref>번째 원소를 찾아라.
        /// </summary>
        /// <typeparam name="T"><code>LinkedListNode</code>의 Generic Type</typeparam>
        /// <param name="head">리스트의 시작지점</param>
        /// <param name="n">찾고자 하는 원소 위치</param>
        /// <returns><paramref name="n"/>번째 원소</returns>
        public LinkedListNode<T> Q2_GetNthNodeFromLast<T>(LinkedListNode<T> head, int n)
        {
            int length = 0;
            var cursor = head;
            while (cursor != null)
            {
                length += 1;
                cursor = cursor.Next;
            }

            cursor = head;
            for (int index = 1; index < length + 1 - n; index++)
            {
                cursor = cursor.Next;
            }

            return cursor;
        }

        /// <summary>
        /// Question 2.3
        /// 중간에 있는 노드 하나를 삭제하라.
        /// (삭제할 노드에 대한 접근만 가능)
        /// </summary>
        /// <typeparam name="T"><code>LinkedListNode</code>의 Generic Type</typeparam>
        /// <param name="target">삭제할 노드</param>
        /// <returns></returns>
        public bool Q3_DeleteNode<T>(LinkedListNode<T> target)
        {
            if (target == null) return false;

            if(target.Prev != null)
            {
                target.Prev.Next = target.Next;
            }

            if(target.Next != null)
            {
                target.Next.Prev = target.Prev;
            }

            target.Prev = null;
            target.Next = null;

            return true;
        }

        /// <summary>
        /// Question 2.4
        /// <paramref name="pivot"/>값을 갖는 노드를 기준으로 연결 리스트를 나눠라.
        /// (<paramref name="pivot"/>보다 작은 값을 갖는 노드가 같거나 더 큰 값을 갖는 노드들보다 앞에 오도록)
        /// </summary>
        /// <typeparam name="T"><code>LinkedListNode</code>의 Generic Type</typeparam>
        /// <param name="head">리스트의 시작지점</param>
        /// <param name="pivot">기준값</param>
        /// <returns>새로운 head</returns>
        public LinkedListNode<int> Partition(LinkedListNode<int> head, int pivot)
        {
            if (head == null) return head;

            //Tail 계산
            var tail = head;
            int count = 1;
            while (tail.Next != null)
            {
                tail = tail.Next;
                count++;
            }

            var node = head;
            for(int index = 0; index < count; index++)
            {
                var next = node.Next;

                if(node.Data >= pivot)
                {
                    tail.Next = node;

                    if (node.Prev != null) node.Prev.Next = node.Next;
                    if (node.Next != null) node.Next.Prev = node.Prev;

                    node.Prev = tail;
                    node.Next = null;

                    tail = node;
                }

                node = next;
            }

            //새로운 Head
            var newHead = tail;
            while (newHead.Prev != null)
            {
                newHead = newHead.Prev;
            }
            return newHead;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="carry"></param>
        /// <returns></returns>
        public LinkedListNode<int> AddLists(LinkedListNode<int> list1, LinkedListNode<int> list2, int carry)
        {
            return null;
        }
    }
}
