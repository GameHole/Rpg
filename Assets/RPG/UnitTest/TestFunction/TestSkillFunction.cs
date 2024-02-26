using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnitTest
{
    internal class TestSkillFunction : TestFunction
    {
        [Test]
        public void testIdleToAttact()
        {
            acts[0].duration = 1.5f;
            for (int i = 0; i < 2; i++)
            {
                anim.log = null;
                input.isAttact = true;
                cha.Update();
                Assert.AreEqual("atk0 ", anim.log);
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                input.isAttact = false;
                cha.Update();
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                cha.Update();
                Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
            }
        }
        [Test]
        public void testIdleToAttactForMove()
        {
            input.moveDir = new Vector2(1, 0);
            input.isAttact = true;
            cha.Update();
            Assert.AreEqual($"{anim.Str("idle")}{anim.Str("atk0")}", anim.log);
            Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
        }
        [Test]
        public void testMoveToAttact()
        {
            acts[0].duration = 1.5f;
            input.moveDir = new Vector2(1, 0);
            cha.Update();
            for (int i = 0; i < 2; i++)
            {
                anim.log = null;
                cha.Update();
                input.isAttact = true;
                cha.Update();
                Assert.AreEqual(anim.Str("atk0"), anim.log);
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                input.isAttact = false;
                cha.Update();
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                cha.Update();
                Assert.AreEqual(typeof(MoveState), matchine.runingState.GetType());
            }
        }
        [Test]
        public void testStopToAttact()
        {
            input.moveDir = new Vector2(1, 0);
            cha.Update();
            anim.log = null;
            input.isAttact = true;
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
        }
        [Test]
        public void testSkillOnePass()
        {
            input.isAttact = true;
            cha.Update();
            input.isAttact = false;
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                cha.Update();
            }
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
        }
        [Test]
        public void testSkillTwoPass()
        {
            input.isAttact = true;
            cha.Update();
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
            }
            Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
            }
            input.isAttact = false;
            cha.Update();
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
        }
        [Test]
        public void testSkillLoop()
        {
            input.isAttact = true;
            for (int i = 0; i < 8; i++)
            {
                cha.Update();
            }
            Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
        }
    }
}
