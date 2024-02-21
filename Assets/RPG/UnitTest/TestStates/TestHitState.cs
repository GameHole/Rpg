using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestHitState:StateTest<HitState>
    {
        [Test]
        public void testHit()
        {
            state.Start();
            Assert.AreEqual("hit", anim.log);
        }
    }
}
