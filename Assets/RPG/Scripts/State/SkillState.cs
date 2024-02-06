﻿using System;
using System.Collections.Generic;

namespace RPG
{
    public enum EnumName { }
    public class ActionState:State
    {
        public Timer action;
        public override void Start()
        {
            action.Reset();
        }
        public override void RunInternal()
        {
            action.Update(character.deltaTime.value);
        }
    }
    public class TransitionToNextSkill : Transition
    {
        public int id;
        internal Timer action;

        public override Enum stateName => (EnumName)id;

        public override bool isVailed()
        {
            return action.isFinish() && character.input.isAttact;
        }
        protected override void RunStateImd(State state) { }
    }
    public class SkillState : State, IFinisher
    {
        public List<Timer> actions { get; } = new List<Timer>();
        public int index { get;private set; }
        private StateMatchine matchine;
        public override void Start()
        {
            character.animator.Attact();
            index = 0;
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new ActionState { action = actions[i] };
                state.transations.Add(new TransitionToNextSkill { action= actions[i], id = (i + 1) % actions.Count });
                matchine.SetState((EnumName)i, state);
            }
            matchine.SetCharacter(character);
            var tran = new TransitionToNextSkill { id = 0 };
            tran.SetMatchine(matchine);
            tran.Switch();
        }
        public override void RunInternal()
        {
            matchine.Update();
        }
        public virtual bool isFinish()
        {
            return (matchine.runingState as ActionState).action.isFinish();
        }
    }
}
