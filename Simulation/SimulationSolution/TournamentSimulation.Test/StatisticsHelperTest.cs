using TournamentSimulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for StatisticsHelperTest and is intended
    ///to contain all StatisticsHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticsHelperTest
    {
        #region Setup
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion

        /// <summary>
        ///A test for MeanAbsoluteDifference
        ///</summary>
        [TestMethod()]
        public void MeanAbsoluteDifferenceTest()
        {
            var s1 = new Helpers.StatisticsHelper(new double[] { 1, 2, 3, 4, 5 });
            var s2 = new Helpers.StatisticsHelper(new double[] { 5, 4, 3, 2, 1 });
            double expected = 2.4;
            var actual = Helpers.StatisticsHelper.MeanAbsoluteDifference(s1, s2);
            Assert.AreEqual(expected, actual);
        }
    }
}
