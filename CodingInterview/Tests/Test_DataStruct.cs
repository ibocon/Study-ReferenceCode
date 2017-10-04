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

        [TestMethod]
        public void Q2()
        {
            Assert.AreEqual(dataStruct.Q2_Reverse("vxyz"), "zyxv");
            Assert.AreEqual(dataStruct.Q2_Reverse("abcde"), "edcba");
            Assert.AreEqual(dataStruct.Q2_Reverse("cat"), "tac");
        }

        [TestMethod]
        public void Q3()
        {
            Assert.IsTrue(dataStruct.Q3_IsPermutation("apple", "papel"));
            Assert.IsTrue(dataStruct.Q3_IsPermutation("carrot", "tarroc"));

            Assert.IsFalse(dataStruct.Q3_IsPermutation("hello", "llloh"));
        }

        [TestMethod]
        public void Q4()
        {
            Assert.AreEqual(dataStruct.Q4_ReplaceSpaces("abc d e f"), "abc%20d%20e%20f");
        }

        [TestMethod]
        public void Q5()
        {
            Assert.AreEqual(dataStruct.Q5_Compress("abbccccccde"), "a1b2c6d1e1");
        }
    }
}
