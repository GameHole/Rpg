using System.Collections.Generic;

namespace RPG
{
    public class ActionClip
    {
        public float duration;
    }
    public class SkillState : State, IFinisher
    {
        public List<ActionClip> actions { get; } = new List<ActionClip>();
        public StateMatchine matchine { get; private set; }
        public override void Start()
        {
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new SkillActionState { id = i };
                state.timer.duration = actions[i].duration;
                state.transations.Add(new TransitionToNextAction((i + 1) % actions.Count, state.timer));
                matchine.SetState(i.ToEnum(), state);
            }
            matchine.SetCharacter(character);
            var tran = new TransitionToNextAction(0,null);
            tran.SetMatchine(matchine);
            tran.Switch();
        }
        public override void RunInternal()
        {
            matchine.Update();
        }
        public virtual bool isFinish()
        {
            return (matchine.runingState as SkillActionState).timer.isFinish();
        }
    }
}
