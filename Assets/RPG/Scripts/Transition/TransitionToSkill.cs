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
        public override void Switch()
        {
            matchine.SetState(StateName.Basic, _this);
            base.Switch();
        }
        protected override Enum stateName => StateName.Skill;
    }
}
