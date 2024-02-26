using System.Collections.Generic;

namespace RPG
{
    public class ActionClip
    {
        public float duration;
        public int id;
        public float hitTime;
        public TargetFilter targetFilter = new TargetFilter();
        public float backTime;
    }
    public class SkillState : State, IFinisher
    {
        public List<ActionClip> actions { get; } = new List<ActionClip>();
        public StateMatchine matchine { get; private set; }
        private TransitionToNextAction switcher = new TransitionToNextAction(0, null);
        public override void Start()
        {
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new SkillActionState { clip = actions[i] };
                state.transations.Add(new TransitionToNextAction((i + 1) % actions.Count, state.timer));
                matchine.SetState(i.ToEnum(), state);
            }
            matchine.SetCharacter(character);
            switcher.SetMatchine(matchine);
            switcher.Switch();
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
