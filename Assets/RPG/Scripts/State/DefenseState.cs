namespace RPG
{
    public class DefenseState : State, IHitter
    {
        public IHitter hitter { get; set; }
        public IRanger ranger { get; set; }
        public override void Start()
        {
            character.animator.Defense();
            hitter = character.hitter;
            character.hitter = this;
        }
        public override void End()
        {
            character.hitter = hitter;
        }
        public void Hit(HitInfo info)
        {
            if (ranger.isInRange(info.hitPoint))
            {
                character.animator.DefenseHit();
                character.strength -= info.demage;
                if (character.strength < 0)
                    character.strength = 0;
            }
            else
            {
                hitter.Hit(info);
            }
        }
    }
}
