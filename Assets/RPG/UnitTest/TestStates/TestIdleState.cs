using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestIdleState:StateTest<IdleState>
    {
        [Test]
        public void test()
        {
            state.Start();
            Assert.AreEqual("idle", anim.log);
        }
    }
}
