using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;
namespace UnitTest
{
    public class TestCharaterFunction
    {
        private TestingAnimator anim;
        private TestingActionInput input;
        private TestDeltaTime deltaTime;
        private Character cha;
        private StateMatchine matchine;
        private Timer[] acts;

        [SetUp]
        public void set()
        {
            anim = new TestingAnimator();
            input = new TestingActionInput();
            deltaTime = new TestDeltaTime();
            cha = new Character(input, anim, deltaTime);
            matchine = cha.matchine;
            var builder = new CharacterStateBuilder();
            builder.Build(cha);
            deltaTime._value = 0.5f;
            var state = cha.matchine.GetState<SkillState>(StateName.Skill);
            acts = new Timer[2];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new Timer() { duration = 2 };
                state.actions.Add(acts[i]);
            }
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
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
            var log = new LogAction();
            matchine.runingState = log;
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
                AssertEx.AreEqualVec2(new Vector2(0.1f*0.5f*(i+1), 0), cha.position);
                Assert.AreEqual(typeof(MoveState), matchine.runingState.GetType());
            }
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
        }
        [Test]
        public void testIdleToAttact()
        {
            acts[0].duration = 1.5f;
            for (int i = 0; i < 2; i++)
            {
                anim.log = null;
                input.isAttact = true;
                cha.Update();
                Assert.AreEqual("atk0", anim.log);
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
            Assert.AreEqual("idleatk0", anim.log);
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
                Assert.AreEqual("atk0", anim.log);
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
                Assert.AreEqual((i + 1) * 0.5f, acts[0].runTime);
                Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
                cha.Update();
                Assert.AreEqual(0, acts[1].runTime);
            }
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
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
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
            Assert.AreEqual(typeof(SkillState), matchine.runingState.GetType());
            for (int i = 0; i < 3; i++)
            {
                cha.Update();
            }
            input.isAttact = false;
            cha.Update();
            Assert.AreEqual(2, acts[1].runTime);
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
            Assert.AreEqual(0, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
            cha.Update();
            Assert.AreEqual(0.5f, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
        }
        [Test]
        public void testHit()
        {
            var hit = matchine.GetState<HitState>(StateName.Hit);
            var timer = hit.timer;
            timer.duration = 1;
            cha.Hit(1);
            cha.Update();
            Assert.AreEqual(typeof(HitState), matchine.runingState.GetType());
            Assert.AreEqual(0.5f, timer.runTime);
            cha.Update();
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
        }
    }
}

