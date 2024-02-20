namespace RPG
{
    public class SkillActionState:AWaitingState
    {
        public int id { get; set; }
        public float hitTime { get; set; }
        public TargetFilter targetFilter { get; set; } = new TargetFilter();
        private bool isHit;
        public override void RunInternal()
        {
            base.RunInternal();
            if (!isHit&&timer.runTime >= hitTime)
            {
                foreach (var item in targetFilter.FindTargets())
                {
                    var damage = character.attact - item.defense;
                    item.Hit(new HitInfo { demage = damage });
                }
                isHit = true;
            }
        }

        protected override void Play()
        {
            character.animator.Attact(id);
            isHit = false;
        }
    }
}
