using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestAWaitingState:StateTest<LogWaitingState>
    {
        [Test]
        public void test()
        {
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual("play", state.log);
        }
    }
}
