using System;

namespace RPG
{
    public class TransitionToHit : Transition
    {
        public override Enum stateName => default;

        public override bool isVailed()
        {
            return false;
        }
    }
}
