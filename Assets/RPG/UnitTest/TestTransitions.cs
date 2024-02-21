using NUnit.Framework;
using RPG;
using UnityEngine;

namespace UnitTest
{
    internal class TestTransitions:StateTesting
    {
        [Test]
        public void testFinishTransitionBlocker()
        {
            var mat = new StateMatchine();
            var timer = new Timer { duration = 1 };
            var tran = new FinishTransitionBlocker(timer);
            tran.SetCharacter(cha);
            tran.SetMatchine(mat);
            for (int i = 0; i < 2; i++)
            {
                timer.Update(0.5f);
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            tran.Switch();
            Assert.IsNull(mat.runingState);
        }
        [Test]
        public void testFinishTransition()
        {
            var timer = new Timer { duration = 1 };
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
        public void testTransitionToHit()
        {
            var tran = new TransitionToHit();
            Assert.AreEqual(StateName.Hit, tran.stateName);
            tran.SetCharacter(cha);
            var hit = new HitInfo { demage = 1 };
            cha.Hit(hit);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(i == 0, tran.isVailed());
            }
            cha.hittable = new Unhittable();
            cha.Hit(hit);
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
            var tran = new TransitionToSkill();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Skill, tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.isAttact = i == 0;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
        }

        [Test]
        public void testTransitionToDead()
        {
            var tran = new TransitionToDead();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Dead, tran.stateName);
            for (int i = -1; i < 2; i++)
            {
                cha.hp = i;
                Assert.AreEqual(i <= 0, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionToRevive()
        {
            var tran = new TransitionToRevive();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Revive, tran.stateName);
            for (int i = -1; i < 2; i++)
            {
                cha.hp = i;
                Assert.AreEqual(i > 0, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionDefenseToIdle()
        {
            var tran = new TransitionDefenseToIdle();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Idle, tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.isDefense = i == 0;
                Assert.AreEqual(i == 1, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionToDefense()
        {
            var tran = new TransitionToDefense();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.Defense, tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.isDefense = i == 0;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
        }
   
        [Test]
        public void testTransitionToBreakDefense()
        {
            var tran = new TransitionToBreakDefense();
            tran.SetCharacter(cha);
            Assert.AreEqual(StateName.BreakDefense, tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                cha.strength = i;
                Assert.AreEqual(i == 0, tran.isVailed());
            }
        }
        [Test]
        public void testTransitionNextAction()
        {
            var mat = new StateMatchine();
            var timer = new Timer { duration = 1 };
            var tran = new TransitionToNextAction(1, timer);
            var log = new LogState();
            mat.SetState(1.ToEnum(), log);
            tran.SetCharacter(cha);
            tran.SetMatchine(mat);
            Assert.AreEqual(1.ToEnum(), tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.isAttact = i == 0;
                for (int j = 0; j < 2; j++)
                {
                    timer.Update(0.5f);
                    Assert.AreEqual(i == 0 && j == 1, tran.isVailed());
                }
            }
            tran.Switch();
            Assert.AreEqual("start ", log.log);
        }
        [Test]
        public void testTransitionToDown()
        {
            var tran = new TransitionToDown();
            Assert.AreEqual(StateName.Down, tran.stateName);
            tran.SetCharacter(cha);
            cha.down.Set();
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(i == 0, tran.isVailed());
            }
        }
    }
}
