namespace RPG
{
    public class BreakDefenseState : State
    {
        public Timer timer { get; } = new Timer();

        public override void Start()
        {
            character.animator.BreakDefense();
            timer.Reset();
        }
        public override void RunInternal()
        {
            timer.Update(character.deltaTime.value);
        }
    }
}
