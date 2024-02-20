using System;

namespace RPG
{
    public class TransitionToBreakDefense : Transition
    {
        public override Enum stateName => StateName.BreakDefense;

        public override bool isVailed()
        {
            return character.strength == 0;
        }
    }
}
