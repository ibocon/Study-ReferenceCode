
namespace Solutions
{
    public class LinkedList
    {
        /// <summary>
        /// Question 2.1
        /// 정렬되지 않은 연결 리스트에서 중복 문자를 제거하라.
        /// (임시 버퍼가 허용되지 않는 상황)
        /// </summary>
        /// <param name="head"></param>
        public void Q1_RemoveDuplicates(LinkedListNode<char> head)
        {
            LinkedListNode<char> a = head;

            while(a.Next != null)
            {
                LinkedListNode<char> b = a;
                
                while(b.Next != null)
                {
                    if(a.Data == b.Next.Data)
                    {
                        b.Next = b.Next.Next;
                        b.Next.Prev = b;
                    }

                    b = b.Next;
                }

                a = a.Next;
            }

        }

    }
}
