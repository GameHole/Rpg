using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class TestDownState: StateTest<DownState>
    {
        [Test]
        public void test()
        {
            state.Start();
            Assert.AreEqual(typeof(NoneHitter), cha.hitter.GetType());
            Assert.AreEqual("down", anim.log);
            state.End();
            Assert.AreSame(cha.hitter, cha.defaultHitter);
        }
    }
}
