using System;

namespace RPG
{
    public class TransitionToBreakDefense : Transition
    {
        public override Enum stateName => default;

        public override bool isVailed()
        {
            return false;
        }
    }
}
