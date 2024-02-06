using System;

namespace RPG
{
    public class TransitionToNextAction : Transition
    {
        public int id;
        public Timer action;

        public override Enum stateName => (EnumName)id;

        public override bool isVailed()
        {
            return action.isFinish() && character.input.isAttact;
        }
        protected override void RunStateImd(State state) { }
    }
}
