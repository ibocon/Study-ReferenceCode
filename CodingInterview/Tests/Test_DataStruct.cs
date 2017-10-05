using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
        public void Q1_1()
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
        public void Q1_2()
        {
            Assert.AreEqual(dataStruct.Q2_Reverse("vxyz"), "zyxv");
            Assert.AreEqual(dataStruct.Q2_Reverse("abcde"), "edcba");
            Assert.AreEqual(dataStruct.Q2_Reverse("cat"), "tac");
        }

        [TestMethod]
        public void Q1_3()
        {
            Assert.IsTrue(dataStruct.Q3_IsPermutation("apple", "papel"));
            Assert.IsTrue(dataStruct.Q3_IsPermutation("carrot", "tarroc"));

            Assert.IsFalse(dataStruct.Q3_IsPermutation("hello", "llloh"));
        }

        [TestMethod]
        public void Q1_4()
        {
            Assert.AreEqual(dataStruct.Q4_ReplaceSpaces("abc d e f"), "abc%20d%20e%20f");
        }

        [TestMethod]
        public void Q1_5()
        {
            Assert.AreEqual(dataStruct.Q5_Compress("abbccccccde"), "a1b2c6d1e1");
        }

        [TestMethod]
        public void Q1_6()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            dataStruct.Q6_Rotate(matrix);

            int[,] expectedAnswer = new int[,] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };

            Assert.AreEqual(matrix.Rank, expectedAnswer.Rank);

            foreach(int dimension in Enumerable.Range(0, matrix.Rank))
            {
                Assert.AreEqual(matrix.GetLength(dimension), expectedAnswer.GetLength(dimension));
            }

            Assert.IsTrue(matrix.Cast<int>().SequenceEqual(expectedAnswer.Cast<int>()));
            
        }

        [TestMethod]
        public void Q1_7()
        {
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 0, 6 }, { 7, 8, 9 } };
            dataStruct.Q07_SetZeros(matrix);

            int[,] expectedAnswer = new int[,] { { 1, 0, 3 }, { 0, 0, 0 }, { 7, 0, 9 } };
            Assert.IsTrue(matrix.Cast<int>().SequenceEqual(expectedAnswer.Cast<int>()));
        }

        [TestMethod]
        public void Q1_8()
        {
            Assert.IsTrue(dataStruct.Q08_IsRotation("apple", "pleap"));
            Assert.IsTrue(dataStruct.Q08_IsRotation("waterbottle", "erbottlewat"));

            Assert.IsFalse(dataStruct.Q08_IsRotation("camera", "macera"));
        }
    }
}
