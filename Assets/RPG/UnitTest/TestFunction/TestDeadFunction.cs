using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RPG;
namespace UnitTest
{
    internal class TestDeadFunction:TestFunction
    {
        [Test]
        public void testIdleToDead()
        {
            input.moveDir = new Vector2(1, 0);
            cha.hp = 0;
            matchine.Update();
            Assert.AreEqual("idle dead ", anim.log);
        }
        [Test]
        public void testMoveToDead()
        {
            input.moveDir = new Vector2(1, 0);
            matchine.Update();
            cha.hp = 0;
            matchine.Update();
            Assert.AreEqual("idle move dead ", anim.log);
        }
        [Test]
        public void testSkillToDead()
        {
            input.isAttact=true;
            matchine.Update();
            cha.hp = 0;
            matchine.Update();
            Assert.AreEqual("idle atk0 dead ", anim.log);
        }
        [Test]
        public void testHitToDead()
        {
            var state = matchine.GetState<HitState>(StateName.Hit);
            state.timer.duration = 1;
            cha.Hit(new HitInfo { demage = 1 });
            matchine.Update();
            cha.hp = 0;
            matchine.Update();
            Assert.AreEqual("idle hit dead ", anim.log);
        }
        [Test]
        public void testBreadDefenseToDead()
        {
            cha.strength = 1;
            var state = matchine.GetState<BreakDefenseState>(StateName.BreakDefense);
            state.timer.duration = 1;
            input.isDefense = true;
            matchine.Update();
            cha.strength = 0;
            matchine.Update();
            cha.hp = 0;
            matchine.Update();
            Assert.AreEqual("idle defense breakdefense dead ", anim.log);
        }
        [Test]
        public void testDeadToIdle()
        {
            var state = matchine.GetState<ReviveState>(StateName.Revive);
            state.timer.duration = 1;
            cha.hp = 0;
            matchine.Update();
            cha.hp = 1;
            matchine.Update();
            Assert.AreEqual("idle dead revive ", anim.log);
            matchine.Update();
            Assert.AreEqual("idle dead revive idle ", anim.log);
        }
    }
}
