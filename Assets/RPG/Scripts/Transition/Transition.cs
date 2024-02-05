using System;

namespace RPG
{
    public abstract class Transition
    {
        public StateMatchine matchine { get; private set; }

        public Character character { get; private set; }

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public virtual void SetMatchine(StateMatchine matchine)
        {
            this.matchine = matchine;
        }
        public virtual void Switch()
        {
            var state = matchine.GetState(stateName);
            state.Start();
            RunStateImd(state);
            matchine.runingState = state;
        }

        protected virtual void RunStateImd(State state)
        {
            state.Run();
        }

        public abstract bool isVailed();

        protected abstract Enum stateName { get; }
    }
}
