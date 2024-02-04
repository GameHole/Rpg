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
        public override void SetCharacter(Character character)
        {
            base.SetCharacter(character);
            character.matchine.SetState(stateName, state);
        }
        protected override Enum stateName => (StateName)0;

    }
}
