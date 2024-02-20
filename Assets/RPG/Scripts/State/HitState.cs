namespace RPG
{
    public abstract class AWaitingState : State
    {
        public Timer timer { get; set; } = new Timer();
        public override void Start()
        {
            Play();
            timer.Reset();
        }

        protected abstract void Play();

        public override void RunInternal()
        {
            timer.Update(character.deltaTime.value);
        }
    }
    public class HitState : AWaitingState
    {
        protected override void Play()
        {
            character.animator.Hit();
        }
    }
}
