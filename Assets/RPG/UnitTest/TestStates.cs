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
            test.SetMatchine(cha.matchine);
            test.Switch();
            Assert.NotNull(test.state.log);
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
        public void testTransitionToIdle()
        {
            var tran = new TransitionToIdle();
            tran.SetCharacter(cha);
            tran.SetMatchine(cha.matchine);
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
            tran.SetMatchine(cha.matchine);
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
        [Test]
        public void testTransitionToBasic()
        {
            var mat = new StateMatchine();
            var basic = new LogState();
            mat.SetState(StateName.Basic, basic);
            var skill = new TestingSkillState();
            var tran = new TransitionToBasic();
            tran.skill = skill;
            tran.SetMatchine(mat);
            for (int i = 0; i < 2; i++)
            {
                skill._isFin = i == 0;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            tran.Switch();
            Assert.AreSame(mat.runingState, basic);
        }
    }
}
