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
            var test = new TestingTransition();
            test.SetCharacter(cha);
            test.Switch();
            Assert.AreSame(test.state, cha.matchine.runingState);
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
        public void testSkill()
        {
            var state = new SkillState();
            state.SetCharacter(cha);
           
            state.Start();
            Assert.AreEqual("atk0", anim.log);
            for (int c = 0; c < 2; c++)
            {
                state.Run();
                Assert.AreEqual(0.5f * (c + 1), state.runTime);
                Assert.AreEqual(0, state.index);
            }
            input.isAttact = false;
            state.Run();
            Assert.AreEqual(2, state.index);
            state.Start();
            Assert.AreEqual(0, state.index);
        }
        [Test]
        public void testTransitionToIdle()
        {
            var tran = new TransitionToIdle();
            tran.SetCharacter(cha);
            for (int i = 0; i < 2; i++)
            {
                input.moveDir = new Vector2(i, 0);
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(cha.matchine.runingState, cha.matchine.GetState(StateName.Idle));
        }
        [Test]
        public void testTransitionToMove()
        {
            var tran = new TransitionToMove();
            tran.SetCharacter(cha);
            for (int i = 0; i < 2; i++)
            {
                input.moveDir = new Vector2(i, 0);
                Assert.AreEqual(i != 0, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(cha.matchine.runingState, cha.matchine.GetState(StateName.Move));
        }
        [Test]
        public void testTransitionToSkill()
        {
            var state = new LogState();
            var tran = new TransitionToSkill();
            tran._this = state;
            tran.SetCharacter(cha);
            for (int i = 0; i < 2; i++)
            {
                input.isAttact = i == 0;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(cha.matchine.runingState, cha.matchine.GetState(StateName.Skill));
            Assert.AreSame(state, cha.matchine.GetState(StateName.Basic));
        }
        [Test]
        public void testTransitionToBasic()
        {
            var logState = new LogState();
            cha.matchine.SetState(StateName.Basic, logState);
            var state = new SkillState();
            state.SetCharacter(cha);
            state.duration = 1;
            var tran = new TransitionToBasic();
            tran.skill = state;
            tran.SetCharacter(cha);
            for (int i = 0; i < 3; i++)
            {
                state.Run();
                Assert.AreEqual(i == 2, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(cha.matchine.runingState, logState);
        }
    }
}
