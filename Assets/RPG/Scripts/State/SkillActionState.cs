namespace RPG
{
    public class SkillActionState:State
    {
        public Timer timer { get; set; }
        public int id { get; set; }
        public float hitTime { get; set; }
        public TargetFilter targetFilter { get; set; } = new TargetFilter();
        private bool isHit;
        public override void Start()
        {
            timer.Reset();
            character.animator.Attact(id);
            isHit = false;
        }
        public override void RunInternal()
        {
            timer.Update(character.deltaTime.value);
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
    }
}
