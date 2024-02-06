using System;

namespace RPG
{
    public class FinishTransitionBlocker : FinishTransition
    {
        public FinishTransitionBlocker(IFinisher finisher) : base(default,finisher)
        {
        }
        public override void Switch() { }
        public override bool isVailed()
        {
            return !base.isVailed();
        }
    }
}
