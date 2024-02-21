using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestReviveState: StateTest<ReviveState>
    {
        [Test]
        public void test()
        {
            cha.hitter = new NoneHitter();
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual("revive", anim.log);
            Assert.AreEqual(0, state.timer.runTime);
            state.End();
            Assert.AreSame(cha.hitter, cha.defaultHitter);
        }
    }
}
