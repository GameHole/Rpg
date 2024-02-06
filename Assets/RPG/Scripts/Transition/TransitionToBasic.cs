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
        public override Enum stateName => StateName.Basic;
    }
}
