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
            clip.hitTime = 1f;
            clip.backTime = 2f;
            input.isAttact = true;
            matchine.GetState<HitState>(StateName.Hit).timer.duration = 1;
        }
        [Test]
        public void testSkillPreparePhase()
        {
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
        [Test]
        public void testSkillAttackPhase()
        {
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
            }
            Assert.AreEqual("idle atk0 ", anim.log);
            cha.Hit(new HitInfo { demage = 1 });
            cha.Update();
            Assert.AreEqual("idle atk0 ", anim.log);
            Assert.AreEqual(1, cha.hp);
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
            }
            Assert.AreEqual("idle atk0 idle ", anim.log);
        }
        [Test]
        public void testSkillBackPhase()
        {
            for (int i = 0; i < 4; i++)
            {
                cha.Update();
            }
            Assert.AreEqual("idle atk0 ", anim.log);
            cha.Hit(new HitInfo { demage = 1 });
            cha.Update();
            Assert.AreEqual("idle atk0 hit ", anim.log);
        }
    }
}
