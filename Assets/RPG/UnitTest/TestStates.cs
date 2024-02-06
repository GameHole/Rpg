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
    internal class TestStates
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
            anim.log = null;
            deltaTime._value = 0.5f;
        }
        [Test]
        public void testTransition()
        {
            var mat = new StateMatchine();
            var test = new TestingTransition();
            test.SetMatchine(mat);
            test.Switch();
            Assert.AreEqual("start run transition ", test.state.log);
            Assert.AreSame(test.state, mat.runingState);
            Assert.AreSame(mat.runingState, mat.GetState(test.stateName));
        }
        [Test]
        public void Move()
        {
            input.moveDir = new Vector2(0.1f, 0);
            var state = new MoveState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("move", anim.log);
            for (int i = 0; i < 2; i++)
            {
                state.Run();
                AssertEx.AreEqualVec2(new Vector2(0.1f*0.5f*(i+1), 0), cha.position);
            }
        }
        [Test]
        public void Idle()
        {
            var state = new IdleState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("idle", anim.log);
        }
        [Test]
        public void testHit()
        {
            var state = new HitState();
            state.SetCharacter(cha);
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual("hit", anim.log);
        }
        [Test]
        public void testTransitionHitToIdle()
        {
            var timer = new Timer { duration=1};
            var tran = new FinishTransition(StateName.Idle, timer);
            Assert.AreEqual(StateName.Idle, tran.stateName);
            tran.SetCharacter(cha);
            for (int i = 0; i < 2; i++)
            {
                timer.Update(0.5f);
                Assert.AreEqual(i == 1, tran.isVailed());
            }
        }
        [Test]
        public void testTimer()
        {
            var timer = new Timer { duration = 1 };
            for (int i = 0; i < 2; i++)
            {
                timer.Update(0.5f);
                Assert.AreEqual(0.5 * (i + 1), timer.runTime);
                Assert.AreEqual(i == 1, timer.isFinish());
            }
        }
        [Test]
        public void testTransitionToHit()
        {
            var tran = new TransitionToHit();
            Assert.AreEqual(StateName.Hit, tran.stateName);
            tran.SetCharacter(cha);
            cha.Hit(1);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            cha.hittable = new Unhittable();
            cha.Hit(1);
            for (int i = 0; i < 2; i++)
            {
                Assert.IsFalse(tran.isVailed());
            }
        }
        [Test]
        public void testTransitionToIdle()
        {
            var tran = new TransitionToIdle();
            Assert.AreEqual(StateName.Idle, tran.stateName);
            tran.SetCharacter(cha);
            for (int i = 0; i < 2; i++)
            {
                input.moveDir = new Vector2(i, 0);
                Assert.AreEqual(i == 0, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionToMove()
        {
            var tran = new TransitionToMove();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Move, tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.moveDir = new Vector2(i, 0);
                Assert.AreEqual(i != 0, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionToSkill()
        {
            var mat = new StateMatchine();
            var skill = new LogState();
            mat.SetState(StateName.Skill,skill);
            var basic = new LogState();
            var tran = new TransitionToSkill();
            tran._this = basic;
            tran.SetCharacter(cha);
            tran.SetMatchine(mat);
            for (int i = 0; i < 2; i++)
            {
                input.isAttact = i == 0;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(mat.runingState, skill);
            Assert.AreSame(basic, mat.GetState(StateName.Basic));
        }
    }
}
