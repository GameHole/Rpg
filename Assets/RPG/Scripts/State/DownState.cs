namespace RPG
{
    public class DownState : AWaitingState
    {
        protected override void Play()
        {
            character.animator.Down();
            character.hitter = new NoneHitter();
        }
        public override void End()
        {
            character.ResetHitter();
        }
    }
}