using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestSkillPhase:TestFunction
    {
        private ActionClip clip;

        public override void set()
        {
            base.set();
            clip = acts[0];
            clip.duration = 3f;
            clip.hitTime = 1.5f;
            input.isAttact = true;
        }
        [Test]
        public void testSkillPreparePhase()
        {
            matchine.GetState<HitState>(StateName.Hit).timer.duration = 1;
            cha.Update();
            Assert.AreEqual("idle atk0 ", anim.log);
            cha.Hit(new HitInfo { demage = 1 });
            cha.Update();
            Assert.AreEqual("idle atk0 hit ", anim.log);
            cha.Update();    
            Assert.AreEqual("idle atk0 hit idle ", anim.log);
            input.isAttact = true;
            cha.Update();
            Assert.AreEqual("idle atk0 hit idle atk0 ", anim.log);
        }
    }
}
