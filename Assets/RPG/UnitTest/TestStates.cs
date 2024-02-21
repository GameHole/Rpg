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
    internal class TestStates:StateTesting
    {
        public override void set()
        {
            base.set();
            anim.log = null;
        }
        [Test]
        public void testMoveState()
        {
            input.moveDir = new Vector2(0.1f, 0);
            var state = new MoveState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("move", anim.log);
            for (int i = 0; i < 2; i++)
            {
                state.RunInternal();
                AssertEx.AreEqualVec2(new Vector2(0.1f*0.5f*(i+1), 0), cha.position);
            }
        }
        [Test]
        public void testIdleState()
        {
            var state = new IdleState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("idle", anim.log);
        }
        [Test]
        public void testWaitingState()
        {
            var state = new LogWaitingState();
            state.SetCharacter(cha);
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual("play", state.log);
        }
        [Test]
        public void testHit()
        {
            var state = new HitState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("hit", anim.log);
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
        public void testDeadState()
        {
            var state = new DeadState();
            state.SetCharacter(cha);
            state.Start();
            Assert.AreEqual("dead", anim.log);
            Assert.AreEqual(typeof(NoneHitter), cha.hitter.GetType());
        }
        [Test]
        public void testReviveState()
        {
            cha.hitter = new NoneHitter();
            var state = new ReviveState();
            state.SetCharacter(cha);
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual("revive", anim.log);
            Assert.AreEqual(0, state.timer.runTime);
            state.End();
            Assert.AreSame(cha.hitter,cha.defaultHitter);
        }
        [Test]
        public void testDefenseState()
        {
            var state = new DefenseState();
            state.SetCharacter(cha);
            var hitter = new NoneHitter();
            cha.hitter = hitter;
            state.Start();
            Assert.AreEqual("defense", anim.log);
            Assert.AreSame(cha.hitter, state);
            Assert.AreSame(hitter, state.hitter);
            state.End();
            Assert.AreSame(hitter,cha.hitter);
        }
        [Test]
        public void testBreakDefenseState()
        {
            var state = new BreakDefenseState();
            state.SetCharacter(cha);
            state.RunInternal();
            Assert.AreEqual(0.5f, state.timer.runTime);
            state.Start();
            Assert.AreEqual(0, state.timer.runTime);
            Assert.AreEqual("breakdefense", anim.log);
        }
    }
}
