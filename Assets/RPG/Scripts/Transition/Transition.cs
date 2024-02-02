namespace RPG
{
    public abstract class Transition
    {
        protected Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public abstract bool isVailed();
        public virtual void Switch()
        {
            character.SwitchTo(getWitchToAction());
        }
        protected abstract State getWitchToAction();
    }
}
