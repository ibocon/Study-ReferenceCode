
namespace Solutions
{
    public class DataStruct
    {
        /// <summary>
        /// Question 1.1
        /// 문자열에 포함된 문자들이 전부 유일한지를 검사하라.
        /// (다른 자료구조를 사용할 수 없는 상황)
        /// </summary>
        /// <param name="input">입력 문자열</param>
        /// <returns>문자열 내의 모든 문자가 전부 유일하면 <c>true</c></returns>
        public bool Q1_IsUniqueChar(string input)
        {
            for(int i = 0; i < input.Length - 1; i++)
            {
                if(input.IndexOf(input[i], i + 1) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Question 1.2
        /// null 문자로 끝나는 문자열을 뒤집어라.
        /// </summary>
        /// <param name="input">입력 문자열</param>
        /// <returns><paramref name="input"/>이 뒤집어진 문자열</returns>
        public string Q2_Reverse(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for(int i = input.Length - 1; 0 <= i; i--)
            {
                sb.Append(input[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Question 1.3
        /// 문자열 두 개를 입력으로 받아 그중 하나가 다른 하나의 순열인지 판별하라.
        /// </summary>
        /// <param name="original">판별기준 문자열</param>
        /// <param name="input">판별대상 문자열</param>
        /// <returns><paramref name="input"/>가 <paramref name="original"/>의 순열이면 <c>true</c></returns>
        public bool Q3_IsPermutation(string original, string input)
        {
            //입력된 문자열에서 다른 문자열과 동일한 문자를 제거하여 순열 판별
            for(int i = 0; i < original.Length; i++)
            {
                for(int j = 0; j < input.Length; j++)
                {
                    if(original[i] == input[j])
                    {
                        input = input.Remove(j, 1);
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Question 1.4
        /// 주어진 문자열 내의 모든 공백을 '%20'으로 변경하라.
        /// </summary>
        /// <param name="input">입력 문자열</param>
        /// <returns><paramref name="input"/>의 모든 공백이 '%20'으로 변환된 값</returns>
        public string Q4_ReplaceSpaces(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach(char c in input)
            {
                if(c == ' ') //주의! char은 single quote로 비교해야 한다.
                {
                    sb.Append("%20");
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Question 1.5
        /// 같은 문자가 연속으로 반복될 경우, 그 횟수를 사용해 문자열을 압축하라.
        /// 만약, 압축할 수 없다면 원래 문자열을 반환하라.
        /// </summary>
        /// <param name="input">입력 문자열</param>
        /// <returns><paramref name="input"/>이 압축된 문자열</returns>
        public string Q5_Compress(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            char check = input[0];
            int count = 1;
            bool compare = false;

            for(int index = 1; index < input.Length; index++)
            {
                if(check == input[index])
                {
                    count++;
                    compare = true;
                }
                else
                {
                    sb.Append(check);
                    sb.Append(count);
                    check = input[index];
                    count = 1;
                }
            }

            sb.Append(check);
            sb.Append(count);

            if (compare)
            {
                return sb.ToString();
            }
            else
            {
                //압축을 시도한 적이 없으므로, 기존 문자열을 그대로 반환
                return input;
            }
        }

        /// <summary>
        /// Question 1.6
        /// 이미지를 표현하는 NxN 행렬이 있다. 이미지의 각 픽셀은 4바이트로 표현된다.
        /// 이때 이미지를 90도 회전시켜라.
        /// (추가 행렬 사용없이 해결)
        /// </summary>
        /// <param name="matrix">입력된 행렬</param>
        public void Q6_Rotate(int[,] matrix)
        {
            int layerCount = matrix.GetLength(0);

            // 만약 제공된 행렬의 길이가 0 보다 작거나, 행과 열의 크기가 다를 경우
            if (layerCount <= 0 || matrix.GetLength(1) != layerCount)
            {
                throw new System.Exception();
            }

            for (var layer = 0; layer < layerCount / 2; ++layer)
            {
                var first = layer;
                var last = layerCount - 1 - layer;

                for (var i = first; i < last; ++i)
                {
                    var offset = i - first;
                    var top = matrix[first, i]; // save top

                    // left -> top
                    matrix[first, i] = matrix[last - offset, first];

                    // bottom -> left
                    matrix[last - offset, first] = matrix[last, last - offset];

                    // right -> bottom
                    matrix[last, last - offset] = matrix[i, last];

                    // top -> right
                    matrix[i, last] = top; // right <- saved top
                }
            }
        }

        /// <summary>
        /// Question 1.7
        /// M x N 행렬을 순회하면서 0인 원소를 발견하면,
        /// 해당 원소가 속한 행과 열의 모든 원소를 0으로 설정하라.
        /// </summary>
        /// <param name="matrix">입력된 행렬</param>
        public void Q07_SetZeros(int[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            bool[] nullifyRow       = new bool[m];
            bool[] nullifyColumn    = new bool[n];

            for(int x = 0; x < m; x++)
            {
                for(int y = 0; y < n; y++)
                {
                    if(matrix[x, y] == 0)
                    {
                        nullifyRow[x]       = true;
                        nullifyColumn[y]    = true;
                    }
                }
            }

            for(int x = 0; x < m; x++)
            {
                if (nullifyRow[x])
                {
                    for(int y = 0; y < n; y++)
                    {
                        matrix[x, y] = 0;
                    }
                }
            }

            for(int y = 0; y < n; y++)
            {
                if (nullifyColumn[y])
                {
                    for(int x = 0; x < m; x++)
                    {
                        matrix[x, y] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Question 1.8 (미해결)
        /// 한 단어가 다른 단어에 포함된 문자열인지 판별하는 <code>isSubstring</code> 함수가 있다고 하자.
        /// <paramref name="s1"/>과 <paramref name="s2"/>의 두 문자열이 주어졌을 때, 
        /// <paramref name="s2"/>가 <paramref name="s1"/>을 회전시킨 결과인지 판별하라.
        /// (<code>isSubstring</code>을 한번만 호출)
        /// </summary>
        /// <param name="s1"><paramref name="s2"/>가 회전한 문자열인지 판별기준</param>
        /// <param name="s2"><paramref name="s1"/>의 회전된 문자열인지 판별대상</param>
        /// <returns><paramref name="s2"/>가 <paramref name="s1"/>를 회전시킨 결과라면 <c>ture</c></returns>
        public bool Q08_IsRotation(string s1, string s2)
        {
            var len = s1.Length;

            /* check that s1 and s2 are equal length and not empty */
            if (len == s2.Length && len > 0)
            {
                /* concatenate s1 and s1 within new buffer */
                var s1S1 = s1 + s1;
                return IsSubstring(s1S1, s2);
            }

            return false;
        }

        /// <summary>
        /// Question 1.8 Helper Func
        /// </summary>
        /// <param name="big"></param>
        /// <param name="small"></param>
        /// <returns></returns>
        private bool IsSubstring(string big, string small)
        {
            return big.IndexOf(small) >= 0;
        }
    }
}
