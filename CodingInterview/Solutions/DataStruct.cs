
namespace Solutions
{
    public class DataStruct
    {
        /// <summary>
        /// Question 1.1
        /// 문자열에 포함된 문자들이 전부 유일한지를 검사하는 알고리즘을 구현하라.
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
        /// 널 문자로 끝나는 문자열을 뒤집는 함수를 구현하라.
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
        /// 문자열 두 개를 입력으로 받아 그중 하나가 다른 하나의 순열인지 판별하는 메서드를 작성하라.
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
        /// 주어진 문자열 내의 모든 공백을 '%20'으로 바구는 메서드를 작성하라.
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
        /// 같은 문자가 연속으로 반복될 경우, 그 횟수를 사용해 문자열을 압축하는 메서드를 구현하라.
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
    }
}
