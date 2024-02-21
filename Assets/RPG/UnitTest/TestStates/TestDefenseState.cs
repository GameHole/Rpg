using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnitTest.StateTest
{
    internal class TestDefenseState:StateTest<DefenseState>
    {
        private LogHitter hitter;
        public override void set()
        {
            base.set();
            hitter = new LogHitter();
            cha.hitter = hitter;
            state.Start();
            state.ranger = new TestingRanger();
        }
        [Test]
        public void test()
        {
            anim.log = null;
            var hitter = new NoneHitter();
            cha.hitter = hitter;
            state.Start();
            Assert.AreEqual("defense", anim.log);
            Assert.AreSame(cha.hitter, state);
            Assert.AreSame(hitter, state.hitter);
            state.End();
            Assert.AreSame(hitter, cha.hitter);
        }
        [Test]
        public void testStateHitOutOfRange()
        {
            state.Hit(new HitInfo { demage = 1, hitPoint = new Vector3(1, 0, 0) });
            Assert.AreEqual("hit1", hitter.log);
        }
        [Test]
        public void testStateHitInRange()
        {
            anim.log = null;
            cha.hp = 1;
            cha.strength = 10;
            var hit = new HitInfo { demage = 1, hitPoint = new Vector3(0, 0, 0) };
            string hitStr = null;
            for (int i = 0; i < 9; i++)
            {
                state.Hit(hit);
                hitStr += "defenseHit";
                Assert.AreEqual(hitStr, anim.log);
                Assert.AreEqual(1, cha.hp);
                Assert.AreEqual(9 - i, cha.strength);
            }
            hit.demage = 2;
            state.Hit(hit);
            Assert.AreEqual(0, cha.strength);
        }
    }
}
