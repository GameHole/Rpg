using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG
{
    public enum StateName
    {
        Idle,Move,Skill,Basic
    }
    public class Character 
    {
        //system
        public IInput input { get; }
        public DeltaTime deltaTime { get; }
        //control
        private Dictionary<Enum, State> stateMap { get; } = new Dictionary<Enum, State>();
        public State GetState(Enum name)
        {
            return stateMap[name];
        }
        public void SetState(Enum name,State state)
        {
            stateMap[name] = state;
        }
        public SkillState skill => GetState(StateName.Skill) as SkillState;
        public State runingState { get; set; }
        public State basicAction { get => GetState(StateName.Basic); set => SetState(StateName.Basic, value); }
        //propertity
        public AAnimator animator { get; }
        public Vector2 position { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;

            var idle = new IdleState();
            idle.transations.Add(new TransitionToSkill { _this = idle });
            idle.transations.Add(new TransitionToMove());
            var move = new MoveState();
            move.transations.Add(new TransitionToSkill { _this = move });
            move.transations.Add(new TransitionToIdle());
            var skill = new SkillState();
            skill.transations.Add(new TransitionToBasic { skill = skill });
            SetState(StateName.Idle, idle);
            SetState(StateName.Move, move);
            SetState(StateName.Skill, skill);
            foreach (var item in stateMap.Values)
            {
                item.SetCharacter(this);
            }
            SwitchTo(idle);
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

