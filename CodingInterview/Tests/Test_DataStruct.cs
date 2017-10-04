using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Test_DataStruct
    {
        private readonly Solutions.DataStruct dataStruct;

        public Test_DataStruct()
        {
            dataStruct = new Solutions.DataStruct();
        }

        [TestMethod]
        public void Q1()
        {
            string[] uniqueWords = { "abcde", "kite", "padle" };
            foreach(string word in uniqueWords)
            {
                Assert.IsTrue(this.dataStruct.Q1_IsUniqueChar(word), $"'{word}' should be unique!");
            }
            string[] commonWords = { "hello", "apple" };
            foreach (string word in commonWords)
            {
                Assert.IsFalse(this.dataStruct.Q1_IsUniqueChar(word), $"'{word}' should not unique!");
            }
        }
    }
}
