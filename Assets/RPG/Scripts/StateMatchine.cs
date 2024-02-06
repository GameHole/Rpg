using System.Collections.Generic;
using System;

namespace RPG
{
    public class StateMatchine
    {
        public State runingState { get; set; }

        private Dictionary<Enum, State> stateMap { get; } = new Dictionary<Enum, State>();
        public State GetState(Enum name)
        {
            return stateMap[name];
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
        public void SwitchToNotRun(Enum stateName)
        {
            var state = GetState(stateName);
            state.Start();
            runingState = state;
        }
        public void SwitchTo(Enum name)
        {
            SwitchTo(GetState(name));
        }
        public void SwitchTo(State state)
        {
            state.Start();
            state.Run();
            runingState = state;
        }

        public void Update()
        {
            runingState.Run();
        }
    }
}

