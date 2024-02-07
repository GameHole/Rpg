namespace RPG
{
    public class HitState : State
    {
        public Timer timer { get; } = new Timer();
        public override void Start()
        {
            Play();
            timer.Reset();
        }

        protected virtual void Play()
        {
            character.animator.Hit();
        }

        public override void RunInternal()
        {
            timer.Update(character.deltaTime.value);
        }
    }
}
