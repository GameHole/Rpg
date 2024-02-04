using System;

namespace RPG
{
    public class TransitionToBasic : Transition
    {
        public SkillState skill;
        public override bool isVailed()
        {
            return skill.isFinish();
        }
        protected override Enum stateName => StateName.Basic;
    }
}
