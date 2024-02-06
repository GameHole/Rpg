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
        private TestingTransition logtran;
        private LogState state;

        [SetUp]
        public void set()
        {
            logtran = new TestingTransition();
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
        public void testTransition()
        {
            var mat = new StateMatchine();
            state.SetMatchine(mat);
            Assert.AreSame(mat, logtran.matchine);
            state.TestTransition();
            Assert.IsNull(logtran.state.log);
            logtran._isVailed = true;
            state.TestTransition();
            Assert.NotNull(logtran.state.log);
            logtran.state.log = null;
            Assert.AreSame(logtran.state, mat.runingState);
            var logtran1 = new TestingTransition() { id=(StateName)1, _isVailed = true,  };
            state.transations.Insert(0, logtran1);
            state.SetMatchine(mat);
            state.TestTransition();
            Assert.IsNull(logtran.state.log);
        }
    }
}
