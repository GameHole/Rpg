using System;

namespace RPG
{
    public class TransitionToSkill: Transition
    {
        public State _this;
        public override bool isVailed()
        {
            return character.input.isAttact;
        }
        protected override State getWitchToAction()
        {
            character.SetState(StateName.Basic, _this);
            return base.getWitchToAction();
        }
        protected override Enum stateName => StateName.Skill;
    }
}
