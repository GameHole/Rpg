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
        private Character cha;

        [SetUp]
        public void set()
        {
            anim = new TestingAnimator();
            input = new TestingActionInput();
            cha = new Character(input, anim);
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
            Assert.AreEqual(typeof(Idle), cha.runingAction.GetType());
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
                Assert.AreEqual(typeof(Move), cha.runingAction.GetType());
            }
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual(typeof(Idle), cha.runingAction.GetType());
        }
        [Test]
        public void testAttact()
        {
            deltaTime = 1;
            input.isAttact = true;
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
                Assert.AreEqual("atki", anim.log);
            }
        }
    }
}

