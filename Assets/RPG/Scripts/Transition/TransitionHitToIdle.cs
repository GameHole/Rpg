using System;

namespace RPG
{
    public class TransitionHitToIdle : Transition
    {
        public override Enum stateName => default;

        public override bool isVailed()
        {
            return false;
        }
    }
}
