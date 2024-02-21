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
        private LogTransition logtran;
        private LogState state;

        [SetUp]
        public void set()
        {
            logtran = new LogTransition();
            state = new LogState();
            state.transations.Add(logtran);
        }
        [Test]
        public void testRun()
        {
            state.Run();
            Assert.AreEqual("run transition ", state.log);
            var cha = new Character(new TestingActionInput(), new TestingAnimator(), new TestDeltaTime());
            state.SetCharacter(cha);
            Assert.AreSame(cha, logtran.character);
            Assert.AreSame(cha, state.character);
        }
        [Test]
        public void testTransitionOrder()
        {
            var mat = new StateMatchine();
            state.SetMatchine(mat);
            Assert.AreSame(mat, logtran.matchine);
            state.TestTransition();
            Assert.IsNull(logtran.log);
            logtran._isVailed = true;
            state.TestTransition();
            Assert.AreEqual("switch",logtran.log);
            logtran.log = null;
            var logtran1 = new LogTransition() {_isVailed = true,  };
            state.transations.Insert(0, logtran1);
            state.SetMatchine(mat);
            state.TestTransition();
            Assert.IsNull(logtran.log);
        }
        [Test]
        public void testTransition()
        {
            var mat = new StateMatchine();
            var test = new TestingTransition();
            test.SetMatchine(mat);
            test.Switch();
            Assert.AreEqual("start run transition ", test.state.log);
            Assert.AreSame(test.state, mat.runingState);
            Assert.AreSame(mat.runingState, mat.GetState(test.stateName));
            test.Switch();
            Assert.AreEqual("start run transition end start run transition ", test.state.log);
        }
    }
}
