using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramAPIAssignment;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestCheckDateTrue()
        {
            var actual = ProgramAPIAssignment.ProgramAPIAssignmentObjectcs.CheckDate("2017-05-06", "2017-05-07");
            Assert.AreEqual(true, actual, "Expected value to be true");
        }

        [TestMethod]
        public void TestCheckDateFalse()
        {
            var actual = ProgramAPIAssignment.ProgramAPIAssignmentObjectcs.CheckDate("2017-05-06", "2017-05-09");
            Assert.AreEqual(false, actual, "Expected value to be true");
        }

        [TestMethod]
        public void TestCheckDateEmpty()
        {
            var actual = ProgramAPIAssignment.ProgramAPIAssignmentObjectcs.CheckDate("", "2017-05-09");
            Assert.AreEqual(false, actual, "Expected value to be true");
        }
    }
}
