using System;

namespace RPG
{
    public class TransitionToNextAction : FinishTransition
    {
        public TransitionToNextAction(int id, IFinisher finisher) : base(id.ToEnum(), finisher)
        {
        }
        public override bool isVailed()
        {
            return base.isVailed() && character.input.isAttact;
        }
        protected override void RunStateImd(State state) { }
    }
}
