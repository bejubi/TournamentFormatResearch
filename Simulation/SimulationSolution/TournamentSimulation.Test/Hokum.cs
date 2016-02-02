using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TournamentSimulation.TournamentStrategies;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra;

namespace TournamentSimulation.Test
{
    /// <summary>
    /// Summary description for Experimental
    /// </summary>
    [TestClass]
    public class Hokum
    {
        #region Setup
        public Hokum()
        {
            //
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        #endregion

        [TestMethod]
        public void TestMultiValuedLists()
        {
            Dictionary<int, int> values = new Dictionary<int, int>();
        }

        [TestMethod()]
        public void TestObjectCreationByClassName()
        {
            TournamentStrategy s1 = new RrTS();

            string className = "Rr";
            Type tournamentStrategyType = Type.GetType(string.Format("TournamentSimulation.TournamentStrategies.{0}TS, TournamentSimulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", className));

            TournamentStrategy strategy = (TournamentStrategy)Activator.CreateInstance(tournamentStrategyType);
        }

        [TestMethod()]
        public void CollectionTest()
        {
            Collection<int> x = new Collection<int>();

            x.Add(1);
            x.Add(2);
            x.Add(3);

            int i = x[0];

            x[0] = 4;
        }

        //[TestMethod()]
        //public void KeyedCollectionTest()
        //{
        //    TestCollection x = new TestCollection();
        //    x.Add(
        //}

        //public class TestCollection : KeyedCollection<string, int>
        //{
        //    protected override string GetKeyForItem(int item)
        //    {
        //        return item;
        //    }
        //}

        [TestMethod()]
        public void RemainderTest()
        {
            //int i = 5 % 8;
        }

        [TestMethod()]
        public void RoundRobinStructureTest()
        {
            List<string> competitors = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };

            GenerateRoundRobinPairings(competitors);
        }

        [TestMethod()]
        public void RoundRobinStructureTest_OddCompetitors()
        {
            List<string> competitors = new List<string>() { "A", "B", "C", "D", "E", "F", "G"};

            GenerateRoundRobinPairings(competitors);
        }

        [TestMethod()]
        public void RoundRobinStructureTest_ManyCompetitors()
        {
            List<string> competitors = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y" };

            GenerateRoundRobinPairings(competitors);
        }

        private static void GenerateRoundRobinPairings(List<string> competitors)
        {
            // fill the indexes the first time, including anything to make an even number of indexes
            List<int> competitorIndexes = new List<int>();
            for (int i = 0; i < competitors.Count + (competitors.Count % 2); i++)
            {
                competitorIndexes.Add(i);
            }

            int numberOfRounds = competitorIndexes.Count - 1;

            // for each round, advance the indexes in the list, except for the last one
            for (int i = 0; i < numberOfRounds; i++)
            {
                // do something for this round
                for (int j = 0; j < competitorIndexes.Count; j++)
                {
                    if ((competitors.Count % 2) == 1 && j == competitorIndexes.Count - 1)
                        continue;

                    Trace.Write(competitors[competitorIndexes[j]]);
                }
                Trace.WriteLine("");

                for (int j = 0; j < competitorIndexes.Count / 2; j++)
                {
                    if ((competitors.Count % 2) == 1 && j == 0)
                        continue;

                    Trace.Write(string.Format("{0} vs. {1}, ", competitors[competitorIndexes[j]], competitors[competitorIndexes[competitorIndexes.Count - j - 1]]));
                }
                Trace.WriteLine("");

                // increment the indexes for the next round
                for (int j = 0; j < competitorIndexes.Count - 1; j++)
                {
                    competitorIndexes[j] = (competitorIndexes[j] + 1) % (competitorIndexes.Count - 1);
                }
            }
        }

        [TestMethod()]
        public void TestRandomIntegers()
        {
            Random randomNumberGenerator;

            for (int j = 0; j < 10; j++)
            {
                randomNumberGenerator = new Random(1001);

                List<int> randomSequence = new List<int>();

                while (randomSequence.Count < 8)
                {
                    int randomNumber = randomNumberGenerator.Next(0, 8);

                    if (!randomSequence.Contains(randomNumber))
                        randomSequence.Add(randomNumber);

                }

                for (int i = 0; i < randomSequence.Count; i++)
                {
                    Trace.Write(string.Format("{0}, ", randomSequence[i]));
                }
                Trace.WriteLine("");
            }
        }

        [TestMethod()]
        public void TestMatrixAddition()
        {
            Matrix m = new Matrix(new double[][] {
                new double[] { 10.0, 0.0 },
                new double[] { 6.0, 4.0 }});

            Matrix n = new Matrix(new double[][] {
                new double[] { 10.0, 0.0 },
                new double[] { 4.0, 6.0 }});

            Trace.WriteLine((m + n).ToString());

            m = new Matrix(3, 3);

            Trace.WriteLine(m.ToString());
        }

    }
}
