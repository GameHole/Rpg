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
            var cha = new Character(new TestingActionInput(),new TestingAnimator(),new TestDeltaTime());
            var logtran = new TestingTransition();
            var state = new LogState();
            state.transations.Add(logtran);
            state.SetCharacter(cha);
            Assert.AreSame(cha,logtran.character);
            Assert.AreSame(cha, state.character);
            state.TestTransition();
            Assert.IsNull(logtran.state.log);
            logtran._isVailed = true;
            state.TestTransition();
            Assert.AreEqual("start run transition ", logtran.state.log);
            logtran.state.log = null;
            Assert.AreSame(logtran.state, cha.matchine.runingState);
            var logtran1 = new TestingTransition() { _isVailed = true };
            logtran1.SetCharacter(cha);
            state.transations.Insert(0, logtran1);
            state.TestTransition();
            Assert.IsNull(logtran.state.log);
        }
    }
}
