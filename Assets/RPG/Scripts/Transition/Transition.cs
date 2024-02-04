using System;

namespace RPG
{
    public abstract class Transition
    {
        public Character character { get; private set; }

        public virtual void SetCharacter(Character character)
        {
            this.character = character;
        }
        public virtual void Switch()
        {
            character.matchine.SwitchTo(stateName);
        }
        public abstract bool isVailed();

        protected abstract Enum stateName { get; }
    }
}
