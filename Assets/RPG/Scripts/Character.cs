using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG
{
    public enum StateName
    {
        Idle,Move,Skill,Basic
    }
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
    public class Character 
    {
        public IInput input { get; }
        public DeltaTime deltaTime { get; }
        public StateMatchine matchine { get; } = new StateMatchine();
        public AAnimator animator { get; }
        public Vector2 position { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;

            BuildStateMatchine();
        }

        private void BuildStateMatchine()
        {
            
        }
        public void Update()
        {
            matchine.Update();
        }
    }
}

