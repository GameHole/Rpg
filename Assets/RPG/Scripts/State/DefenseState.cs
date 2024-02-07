namespace RPG
{
    public class DefenseState : State
    {
        public DefenseHitter hitter { get; set; }

        public override void Start()
        {
            character.animator.Defense();
        }
    }
}
