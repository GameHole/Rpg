using System;

namespace RPG
{
    public class TransitionDefenseToIdle : Transition
    {
        public override Enum stateName => StateName.Idle;

        public override bool isVailed()
        {
            return !character.input.isDefense;
        }
    }
}
