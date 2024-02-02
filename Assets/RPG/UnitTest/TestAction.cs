using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;
namespace UnitTest
{
    public class TestAction
    {
        private TestingAnimator anim;
        private TestingActionInput input;
        private TestDeltaTime deltaTime;
        private Character cha;

        [SetUp]
        public void set()
        {
            anim = new TestingAnimator();
            input = new TestingActionInput();
            deltaTime = new TestDeltaTime();
            cha = new Character(input, anim, deltaTime);
            cha.skill.duration = 1;
            deltaTime._value = 0.5f;
        }
        [Test]
        public void testAnimator()
        {
            for (int i = 0; i < 2; i++)
            {
                cha.Update();
                Assert.AreEqual("idle", anim.log);
            }
            input.moveDir = new Vector2(0.1f, 0);
            for (int i = 0; i < 2; i++)
            {
                cha.Update();
                Assert.AreEqual("idlemove", anim.log);
            }
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual("idlemoveidle", anim.log);
            input.moveDir = new Vector2(0.1f, 0);
            cha.Update();
            Assert.AreEqual("idlemoveidlemove", anim.log);
        }
        [Test]
        public void testDefault()
        {
            Assert.AreEqual(typeof(IdleState), cha.runingAction.GetType());
            var log = new LogAction();
            cha.runingAction = log;
            cha.Update();
            Assert.AreEqual("run", log.log);
        }
        [Test]
        public void testMove()
        {
            input.moveDir = new Vector2(0.1f, 0);
            AssertEx.AreEqualVec2(new Vector2(0, 0), cha.position);
            for (int i = 0; i < 2; i++)
            {
                cha.Update();
                AssertEx.AreEqualVec2(new Vector2(0.1f*(i+1), 0), cha.position);
                Assert.AreEqual(typeof(MoveState), cha.runingAction.GetType());
            }
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual(typeof(IdleState), cha.runingAction.GetType());
        }
        [Test]
        public void testIdleToAttact()
        {
            for (int i = 0; i < 2; i++)
            {
                anim.log = null;
                input.isAttact = true;
                cha.Update();
                Assert.AreEqual("atk", anim.log);
                Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
                input.isAttact = false;
                cha.Update();
                Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
                cha.Update();
                Assert.AreEqual(typeof(IdleState), cha.runingAction.GetType());
            }
        }
        [Test]
        public void testIdleToAttactForMove()
        {
            input.moveDir = new Vector2(1, 0);
            input.isAttact = true;
            cha.Update();
            Assert.AreEqual("idleatk", anim.log);
            Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
        }
        [Test]
        public void testMoveToAttact()
        {
            input.moveDir = new Vector2(1, 0);
            cha.Update();
            for (int i = 0; i < 2; i++)
            {
                anim.log = null;
                cha.Update();
                input.isAttact = true;
                cha.Update();
                Assert.AreEqual("atk", anim.log);
                Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
                input.isAttact = false;
                cha.Update();
                Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
                cha.Update();
                Assert.AreEqual(typeof(MoveState), cha.runingAction.GetType());
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
            Assert.AreEqual(typeof(SkillState), cha.runingAction.GetType());
            Assert.AreEqual("atk", anim.log);
        }
    }
}

