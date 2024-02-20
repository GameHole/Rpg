namespace RPG
{
    public class HitState : AWaitingState
    {
        protected override void Play()
        {
            character.animator.Hit();
        }
    }
}
