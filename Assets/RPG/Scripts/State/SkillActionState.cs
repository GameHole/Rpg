namespace RPG
{
    public class SkillActionState:AWaitingState
    {
        public ActionClip clip { get; set; }
        private bool isHit;
        public override void RunInternal()
        {
            base.RunInternal();
            if (!isHit&&timer.runTime >= clip.hitTime)
            {
                foreach (var item in clip.targetFilter.FindTargets())
                {
                    var damage = character.attact - item.defense;
                    item.Hit(new HitInfo { demage = damage });
                }
                isHit = true;
            }
        }

        protected override void Play()
        {
            character.animator.Attact(clip.id);
            timer.duration = clip.duration;
            isHit = false;
        }
    }
}
