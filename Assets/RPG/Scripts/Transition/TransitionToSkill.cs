using System;

namespace RPG
{
    public class TransitionToSkill: Transition
    {
        public override bool isVailed()
        {
            return character.input.isAttact;
        }
        public override Enum stateName => StateName.Skill;
    }
}
