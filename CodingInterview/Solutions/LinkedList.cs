
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
        public LinkedListNode<int> Q4_Partition(LinkedListNode<int> head, int pivot)
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
        /// Question 2.5
        /// 연결 리스트로 표현된 두 개의 수가 있다고 하자. 리스트의 각 노드는 해당 수의 각 자릿수를 표현한다.
        /// (이 때 자릿수들은 역순으로 배열되는데, 1의 자릿수가 리스트의 맨 앞에 오도록 배열된다는 뜻이다.)
        /// 이 두 수를 더하여 그 합을 연결 리스트로 반환하는 함수를 작성하라.
        /// </summary>
        /// <param name="number1">첫번째 수</param>
        /// <param name="number2">두번째 수</param>
        /// <param name="carry">자리넘김</param>
        /// <returns><paramref name="number1"/>와 <paramref name="number2"/>를 합한 수. 잘못된 값이 입력되었을 경우 <c>null</c>을 반환한다.</returns>
        public LinkedListNode<int> Q5_AddNumbers(LinkedListNode<int> number1, LinkedListNode<int> number2, bool carry)
        {
            if (number1 == null && number2 == null) return null;

            int value = ((carry) ? 1 : 0);
            if (number1 != null) value += number1.Data;
            if (number2 != null) value += number2.Data;
            
            bool quotient = (value / 10) > 0;
            int remainder = value % 10;
            LinkedListNode<int> result = new LinkedListNode<int>(remainder, this.Q5_AddNumbers( number1?.Next, number2?.Next, carry), null);

            return result;
        }

        /// <summary>
        /// Question 2.6 (미해결)
        /// 순환 연결 리스트가 주어졌을 때, 순환되는 부분의 첫 노드를 반환하라.
        /// </summary>
        /// <typeparam name="T"><code>LinkedListNode</code>의 Generic Type</typeparam>
        /// <param name="head">리스트의 시작 노드</param>
        /// <returns>순환되는 부분의 첫 노드</returns>
        public LinkedListNode<T> Q6_FindBeginning<T>(LinkedListNode<T> head)
        {
            var slow = head;
            var fast = head;

            // 충돌 지점을 찾는다. 연결 리스트 안으로 LOOP_SIZE - k만큼 들어간 상태가 된다.
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (slow == fast)
                {
                    break;
                }
            }

            // 오류 검사. 충돌이 없다면, 루프도 없다.
            if (fast == null || fast.Next == null)
            {
                return null;
            }

            /*
             *  slow를 head로 이동시킨다. fast는 충돌 지점에 그대로 둔다.
             *  그 둘은 루프 시작 지점에서 k만큼 떨어져 있다.
             *  그러므로 같은 속도로 움직이면, 시작점에서 만나게 된다.
             */
            slow = head;
            while (slow != fast)
            {
                slow = slow.Next;
                fast = fast.Next;
            }

            // 둘 다 루프 시작점을 가리키게 된다.
            return fast;
        }

        /// <summary>
        /// Question 2.7
        /// 주어진 연결 리스트가 회문인지 검사하는 함수를 작성하라.
        /// </summary>
        /// <param name="head">리스트의 시작 노드</param>
        /// <returns>회문검사 결과</returns>
        public bool Q7_IsPalindrome(LinkedListNode<int> head)
        {
            var fast = head;
            var slow = head;

            var stack = new System.Collections.Generic.Stack<int>();

            /*
             *  연결 리스트를 Stack에 쌓는다. 
             *  (처음 구현할 때는 모든 원소를 쌓았으나, 책 정답을 반영하여 절반만 쌓는다.)
             *  fast와 slow 방법을 활용하여 원소 중간 지점을 체크한다.
             */ 
            while (fast != null && fast.Next != null)
            {
                stack.Push(slow.Data);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

             
            //  리스트 길이가 홀수라면, 가운데 원소는 매칭하는 원소가 자기 자신이므로 제외한다.
            if (fast != null)
            {
                slow = slow.Next;
            }

            while (slow != null)
            {
                var top = stack.Pop();

                //  값이 다르면 회문 리스트가 아니다.
                if (top != slow.Data)
                {
                    return false;
                }
                slow = slow.Next;
            }

            return true;
        }

    }
}
