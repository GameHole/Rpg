namespace RPG
{
    public class BreakDefenseState : AWaitingState
    {
        protected override void Play()
        {
            character.animator.BreakDefense();
        }
    }
}
