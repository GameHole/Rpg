namespace RPG
{
    public class HitState : State
    {
        public Timer timer { get; } = new Timer();
        public override void Start()
        {
            character.animator.Hit();
            timer.Reset();
        }
        public override void RunInternal()
        {
            timer.Update(character.deltaTime.value);
        }
    }
}
