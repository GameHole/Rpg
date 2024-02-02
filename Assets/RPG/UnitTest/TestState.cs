using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestState
    {
        [Test]
        public void testRun()
        {
            var state = new LogState();
            state.Run();
            Assert.AreEqual("run transition ", state.log);
        }
        [Test]
        public void testTransition()
        {
            var cha = new Character();
            var logtran = new TestingTransition();
            var state = new LogState();
            state.transations.Add(logtran);
            state.SetCharacter(cha);
            Assert.AreSame(logtran.charater, cha);
            state.TestTransition();
            Assert.AreEqual("run transition ", state.log);
        }
    }
}
