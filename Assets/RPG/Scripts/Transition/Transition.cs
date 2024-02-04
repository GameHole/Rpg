namespace RPG
{
    public abstract class Transition
    {
        public Character character { get; private set; }

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
