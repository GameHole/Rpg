using System;

namespace RPG
{
    public interface IFinisher
    {
        bool isFinish();
    }
    public class FinishTransition : Transition
    {
        public override Enum stateName => _stateName;
        public IFinisher finisher;
        private Enum _stateName;
        public FinishTransition(Enum stateName,IFinisher finisher)
        {
            _stateName = stateName;
            this.finisher = finisher;
        }
        public override bool isVailed()
        {
            return finisher.isFinish();
        }
    }
}
