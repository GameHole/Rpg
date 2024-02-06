using System.Collections.Generic;
using System;

namespace RPG
{
    [Serializable]
    public class StateNameNotFoundException : Exception
    {
        public StateNameNotFoundException() { }
        public StateNameNotFoundException(string message) : base(message) { }
        public StateNameNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected StateNameNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class StateMatchine
    {
        public State runingState { get; set; }

        private Dictionary<Enum, State> stateMap { get; } = new Dictionary<Enum, State>();
        public State GetState(Enum name)
        {
            if (!stateMap.TryGetValue(name, out var state))
                throw new StateNameNotFoundException(name.ToString());
            return state;
        }
        public T GetState<T>(Enum name) where T : State
        {
            return GetState(name) as T;
        }
        public void SetState(Enum name, State state)
        {
            stateMap[name] = state;
            state.SetMatchine(this);
        }
        public void SetCharacter(Character character)
        {
            foreach (var item in stateMap.Values)
            {
                item.SetCharacter(character);
            }
        }

        public void Update()
        {
            runingState.Run();
        }
    }
}

