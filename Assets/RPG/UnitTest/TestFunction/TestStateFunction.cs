using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RPG;
using UnityEngine;
using UnityEngine.TestTools;
namespace UnitTest
{
    class TestStateFunction: TestFunction
    {
        [Test]
        public void testAnimator()
        {
            for (int i = 0; i < 2; i++)
            {
                cha.Update();
                Assert.AreEqual("idle ", anim.log);
            }
            input.moveDir = new Vector2(0.1f, 0);
            for (int i = 0; i < 2; i++)
            {
                cha.Update();
                Assert.AreEqual("idle move ", anim.log);
            }
            input.moveDir = new Vector2(0, 0);
            cha.Update();
            Assert.AreEqual("idle move idle ", anim.log);
            input.moveDir = new Vector2(0.1f, 0);
            cha.Update();
            Assert.AreEqual($"{anim.Str("idle")}{anim.Str("move")}{anim.Str("idle")}{anim.Str("move")}", anim.log);
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
        public void testHit()
        {
            var hit = matchine.GetState<HitState>(StateName.Hit);
            var timer = hit.timer;
            timer.duration = 1;
            cha.Hit(new HitInfo { demage = 1 });
            cha.Update();
            Assert.AreEqual(typeof(HitState), matchine.runingState.GetType());
            Assert.AreEqual(0.5f, timer.runTime);
            cha.Update();
            Assert.AreEqual(typeof(IdleState), matchine.runingState.GetType());
        }
    }
}

