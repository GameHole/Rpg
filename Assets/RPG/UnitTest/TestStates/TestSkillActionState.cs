using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestSkillActionState: StateTest<SkillActionState>
    {
        [Test]
        public void testState()
        {
            state.id = 1;
            var timer = new Timer();
            state.timer = timer;
            state.RunInternal();
            Assert.AreEqual(0.5f, timer.runTime);
            state.Start();
            Assert.AreEqual(0, timer.runTime);
            Assert.AreEqual(anim.Str("atk1"), anim.log);
        }
        [Test]
        public void testHit()
        {
            var timer = new Timer() { duration = 2 };
            state.timer = timer;
            var filter = new TestingTargetFilter();
            state.hitTime = 1;
            state.targetFilter = filter;
            for (int c = 0; c < 2; c++)
            {
                cha.attact = 10;
                filter.target.hp = 10;
                filter.target.defense = 1;
                filter.target.log = null;
                state.Start();
                state.RunInternal();
                Assert.IsNull(filter.target.log);
                for (int i = 0; i < 4; i++)
                {
                    state.RunInternal();
                    Assert.AreEqual("hit9", filter.target.log);
                }
            }

        }
    }
}
