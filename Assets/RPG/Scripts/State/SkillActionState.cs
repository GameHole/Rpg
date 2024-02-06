namespace RPG
{
    public class SkillActionState:State
    {
        public Timer action;
        public int id { get; set; }
        public float hitTime { get; set; }
        public TargetFilter targetFilter { get; set; } = new TargetFilter();
        private bool isHit;
        public override void Start()
        {
            action.Reset();
            character.animator.Attact(id);
            isHit = false;
        }
        public override void RunInternal()
        {
            action.Update(character.deltaTime.value);
            if (!isHit&&action.runTime >= hitTime)
            {
                foreach (var item in targetFilter.FindTargets())
                {
                    var hit = character.attact - item.defense;
                    item.Hit(hit);
                }
                isHit = true;
            }
        }
    }
}
