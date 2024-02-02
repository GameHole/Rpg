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
            character.basicAction = _this;
            return character.skill;
        }
    }
}
