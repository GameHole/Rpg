using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestDeadState: StateTest<DeadState>
    {
        [Test]
        public void testDeadState()
        {
            state.Start();
            Assert.AreEqual(anim.Str("dead"), anim.log);
            Assert.AreEqual(typeof(NoneHitter), cha.hitter.GetType());
        }
    }
}
