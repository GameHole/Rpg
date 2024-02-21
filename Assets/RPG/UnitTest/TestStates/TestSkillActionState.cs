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
        private ActionClip clip;

        public override void set()
        {
            base.set();
            clip = new ActionClip();
            state.clip = clip;
        }
        [Test]
        public void testState()
        {
            clip.id = 1;
            clip.duration = 1;
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(1, state.timer.duration);
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual(anim.Str("atk1"), anim.log);
        }
        [Test]
        public void testHit()
        {
            state.timer.duration = 2;
            var filter = new TestingTargetFilter();
            clip.hitTime = 1;
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
