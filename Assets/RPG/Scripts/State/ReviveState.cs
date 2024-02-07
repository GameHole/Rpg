using System;

namespace RPG
{
    public class ReviveState : HitState
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
