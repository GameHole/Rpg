namespace RPG
{
    public class TransitionToBasic : Transition
    {
        public SkillState skill;
        public override bool isVailed()
        {
            return skill.runTime > skill.duration;
        }
        protected override State getWitchToAction() => character.basicAction;
    }
}
