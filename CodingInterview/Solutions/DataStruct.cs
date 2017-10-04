using System;

namespace Solutions
{
    public class DataStruct
    {
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
    }
}
