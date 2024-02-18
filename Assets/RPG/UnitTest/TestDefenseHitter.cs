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
    internal class TestDefenseHitter
    {
        private LogHitter hitter;
        private DefenseHitter def;
        private TestingAnimator anim;
        private Character cha;

        [SetUp]
        public void set()
        {
            anim = new TestingAnimator();
            cha = new Character(new TestingActionInput(), anim, new TestDeltaTime());
            hitter = new LogHitter();
            cha.hitter = hitter;
            def = new DefenseHitter(cha);
            def.Decorate();
            def.ranger = new TestingRanger();
        }
        [Test]
        public void testHitOutOfRange()
        {
            def.Hit(new HitInfo { demage = 1, hitPoint = new Vector3(1,0,0) });
            Assert.AreEqual("hit1", hitter.log);
        }
        [Test]
        public void testHitInRange()
        {
            cha.hp = 1;
            cha.strength = 10;
            var hit = new HitInfo { demage = 1, hitPoint = new Vector3(0, 0, 0) };
            string hitStr = null;
            for (int i = 0; i < 9; i++)
            {
                def.Hit(hit);
                hitStr += "defenseHit";
                Assert.AreEqual(hitStr, anim.log);
                Assert.AreEqual(1, cha.hp);
                Assert.AreEqual(9 - i, cha.strength);
            }
            hit.demage = 2;
            def.Hit(hit);
            Assert.AreEqual(0, cha.strength);
        }
    }
}
