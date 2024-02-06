using System.Collections.Generic;

namespace RPG
{
    public enum EnumName { }
    public class SkillState : State, IFinisher
    {
        public List<Timer> actions { get; } = new List<Timer>();
        public int index { get;private set; }
        public StateMatchine matchine { get; private set; }
        public override void Start()
        {
            index = 0;
            matchine = new StateMatchine();
            for (int i = 0; i < actions.Count; i++)
            {
                var state = new SkillActionState { action = actions[i], id=i };
                state.transations.Add(new TransitionToNextAction { action= actions[i], id = (i + 1) % actions.Count });
                matchine.SetState(i.ToEnum(), state);
            }
            matchine.SetCharacter(character);
            var tran = new TransitionToNextAction { id = 0 };
            tran.SetMatchine(matchine);
            tran.Switch();
        }
        public override void RunInternal()
        {
            matchine.Update();
        }
        public virtual bool isFinish()
        {
            return (matchine.runingState as SkillActionState).action.isFinish();
        }
    }
}
