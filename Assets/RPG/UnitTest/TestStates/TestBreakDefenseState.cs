using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestBreakDefenseState: StateTest<BreakDefenseState>
    {
        [Test]
        public void testBreakDefenseState()
        {
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual("breakdefense", anim.log);
        }
    }
}
