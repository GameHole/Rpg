using System;
using System.Collections.Generic;

namespace RPG
{
    public enum EnumName { }
    public class ActionState:State
    {
        public SkillAction action;
        public override void Start()
        {
            action.Reset();
        }
        protected override void RunInternal()
        {
            action.Update(character.deltaTime.value);
        }
    }
    public class TransitionToNextSkill : Transition
    {
        public int id;
        internal ActionState state;

        protected override Enum stateName => (EnumName)id;

        public override bool isVailed()
        {
            return state.action.isFinish()&&character.input.isAttact;
        }
        public override void Switch()
        {
            var state = matchine.GetState(stateName);
            state.Start();
            matchine.runingState = state;
        }
    }
    public class SkillState : State
    {
        public float duration;
        public float runTime { get; private set; }
        public List<SkillAction> actions { get; } = new List<SkillAction>();
        public int index { get;private set; }
        private StateMatchine matchine;
        public override void Start()
        {
            character.animator.Attact();
            index = 0;
            foreach (var item in actions)
            {
                item.Reset();
            }
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new ActionState { action = actions[i] };
                state.transations.Add(new TransitionToNextSkill { state=state, id = (i + 1) % actions.Count });
                matchine.SetState((EnumName)i, state);
            }
            matchine.SetCharacter(character);
            matchine.runingState=matchine.GetState((EnumName)0);
        }

        

        protected override void RunInternal()
        {
            matchine.Update();
        }

       

        public bool isFinish()
        {
            return (matchine.runingState as ActionState).action.isFinish();
        }

        private bool isLastest()
        {
            return index >= actions.Count;
        }
    }
}
