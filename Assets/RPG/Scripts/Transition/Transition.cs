using System;

namespace RPG
{
    public abstract class Transition
    {
        public State transititeToState;
        public Character character { get; private set; }

        public virtual void SetCharacter(Character character)
        {
            this.character = character;
        }
        public abstract bool isVailed();
        public virtual void Switch()
        {
            character.SwitchTo(getWitchToAction());
        }
        protected virtual State getWitchToAction() => character.GetState(stateName);
        protected abstract Enum stateName { get; }
    }
}
