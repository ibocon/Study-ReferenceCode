using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions
{
    /// <summary>
    /// Chapter 03 스택과 큐
    /// </summary>
    public class StackQueue
    {
        /// <summary>
        /// 각 스택에 대한 메타데이터를 관리한다.
        /// </summary>
        internal class StackData
        {
            /// <summary>
            /// 전체 배열에서 스택 시작점
            /// </summary>
            public int Start { get; set; }
            /// <summary>
            /// 현재 스택 포인터
            /// </summary>
            public int Pointer { get; set; }
            /// <summary>
            /// 스택에 저장된 데이터 크기
            /// </summary>
            public int Size { get; set; }
            /// <summary>
            /// 스택이 가질 수 있는 데이터 용량
            /// </summary>
            public int Capacity { get; set; }

            /// <summary>
            /// 스택 메타 데이터 인스턴스 생성
            /// </summary>
            /// <param name="start">전체 배열에서 스택 시작지점</param>
            /// <param name="capacity">스택 허용 용량</param>
            public StackData(int start, int capacity)
            {
                this.Start = start;
                this.Pointer = start - 1;
                this.Size = 0;
                this.Capacity = capacity;
            }

            /// <summary>
            /// 현재 스택 내에 값이 존재하는지 체크한다.
            /// </summary>
            /// <param name="index">스택 내부인지 확인할 기준 위치</param>
            /// <param name="totalSize">스택이 포함되어 있는 배열 크기</param>
            /// <returns>스택 내부에 있으면 <code>true</code> 반환</returns>
            public bool IsWithInStack(int index, int totalSize)
            {
                if (this.Start <= index && index < this.Start + this.Capacity)
                {
                    return true;
                }
                else if (this.Start + this.Capacity > totalSize && index < (this.Start + this.Capacity) % totalSize)
                {
                    return true;
                }

                return false;
            }
        }

        public void Q1_CreateThreeStacks()
        {
            
        }
    }
}
