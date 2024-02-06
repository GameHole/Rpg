using System;

namespace RPG
{
    public class TransitionHitToIdle : Transition
    {
        public Timer timer;

        public override Enum stateName => StateName.Idle;

        public override bool isVailed()
        {
            return timer.isFinish();
        }
    }
}
