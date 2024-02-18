using System.Collections.Generic;

namespace RPG
{
    public class SkillState : State, IFinisher
    {
        public List<Timer> actions { get; } = new List<Timer>();
        public StateMatchine matchine { get; private set; }
        public override void Start()
        {
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new SkillActionState { timer = actions[i], id=i };
                state.transations.Add(new TransitionToNextAction((i + 1) % actions.Count,actions[i]));
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
