using System;

namespace RPG
{
    public class ReviveState : AWaitingState
    {
        protected override void Play()
        {
            character.animator.Revive();
        }
        public override void End()
        {
            character.ResetHitter();
        }
    }
}
