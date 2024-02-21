using System;

namespace RPG
{
    public class TransitionToDown : Transition
    {
        public override Enum stateName => default;

        public override bool isVailed()
        {
            return false;
        }
    }
}
