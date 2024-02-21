using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestDefenseFunction:TestFunction
    {
        [Test]
        public void testDefenseNoStrength()
        {
            var state = matchine.GetState<BreakDefenseState>(StateName.BreakDefense);
            state.timer.duration = 1;
            input.isDefense = true;
            for (int i = 0; i < 3; i++)
            {
                matchine.Update();
            }
            Assert.AreEqual("idle defense ", anim.log);
        }
        [Test]
        public void testDefense()
        {
            cha.strength = 1;
            input.isDefense = true;
            for (int i = 0; i < 3; i++)
            {
                matchine.Update();
            }
            Assert.AreEqual("idle defense ", anim.log);
            input.isDefense = false;
            matchine.Update();
            Assert.AreEqual("idle defense idle ", anim.log);
        }
        [Test]
        public void testBreakDefense()
        {
            cha.strength = 1;
            var state = matchine.GetState<BreakDefenseState>(StateName.BreakDefense);
            state.timer.duration = 1;
            input.isDefense = true;
            matchine.Update();
            cha.strength = 0;
            input.isDefense = false;
            matchine.Update();
            matchine.Update();
            Assert.AreEqual("idle defense breakdefense idle ", anim.log);
            input.isDefense = true;
            matchine.Update();
            Assert.AreEqual("idle defense breakdefense idle defense ", anim.log);
        }
    }
}
