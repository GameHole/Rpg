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
        public Enum id = (StateName)0;
        public LogState state = new LogState();
        public bool _isVailed;
        public override bool isVailed() => _isVailed;
        public override void SetMatchine(StateMatchine matchine)
        {
            base.SetMatchine(matchine);
            matchine.SetState(stateName, state);
        }
        public override Enum stateName => id;

    }
}
