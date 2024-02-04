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
        }
        public void SetCharacter(Character character)
        {
            foreach (var item in stateMap.Values)
            {
                item.SetCharacter(character);
            }
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
        //system
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
            var idle = new IdleState();
            idle.transations.Add(new TransitionToSkill { _this = idle });
            idle.transations.Add(new TransitionToMove());
            var move = new MoveState();
            move.transations.Add(new TransitionToSkill { _this = move });
            move.transations.Add(new TransitionToIdle());
            var skill = new SkillState();
            skill.transations.Add(new TransitionToBasic { skill = skill });
            matchine.SetState(StateName.Idle, idle);
            matchine.SetState(StateName.Move, move);
            matchine.SetState(StateName.Skill, skill);
            matchine.SetCharacter(this);
            matchine.SwitchTo(idle);
        }
        public void Update()
        {
            matchine.Update();
        }
    }
}

