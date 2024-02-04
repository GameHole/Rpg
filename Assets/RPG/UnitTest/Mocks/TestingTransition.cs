using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestingTransition : Transition
    {
        public LogState state = new LogState();
        public bool _isVailed;
        public override bool isVailed() => _isVailed;

        protected override State getWitchToAction()
        {
            return state;
        }
    }
}
