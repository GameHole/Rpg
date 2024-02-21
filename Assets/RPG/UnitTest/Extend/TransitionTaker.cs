using RPG;
using System;

namespace UnitTest
{
    class TransitionTaker
    {
        public State state { get; private set; }
        int index = 0;
        public TransitionTaker(State state)
        {
            this.state = state;
        }

        public int TransitionCount => state.transations.Count;

        public Transition Next()
        {
            return state.transations[index++];
        }

        public T Next<T>() where T : Transition => Next() as T;

        public Type NextType() => Next().GetType();
    }
}
