using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestStateMatchine
    {
        private StateMatchine mat;
        private LogState state;

        enum TestName { name }
        [SetUp]
        public void set()
        {
            mat = new StateMatchine();
            state = new LogState();
        }
        [Test]
        public void testGetState()
        {
            Assert.Throws<KeyNotFoundException>(() => mat.GetState(TestName.name));
        }
        [Test]
        public void testSetState()
        {
            var tran = new TestingTransition();
            state.transations.Add(tran);
            mat.SetState(TestName.name, state);
            Assert.AreSame(mat, tran.matchine);
            Assert.AreSame(state, mat.GetState(TestName.name));
            Assert.AreSame(state, mat.GetState<LogState>(TestName.name));
        }
        [Test]
        public void testSwitchTo()
        {
            mat.SwitchTo(state);
            Assert.AreEqual("start run transition ", state.log);
            Assert.AreSame(mat.runingState, state);
        }
        [Test]
        public void testSwitchToByName()
        {
            mat.SetState(TestName.name, state);
            mat.SwitchTo(TestName.name);
            Assert.AreSame(mat.runingState, state);
        }
        [Test]
        public void testUpdate()
        {
            mat.runingState = state;
            mat.Update();
            Assert.AreEqual("run transition ", state.log);
        }
    }
}
