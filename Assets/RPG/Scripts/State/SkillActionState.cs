namespace RPG
{
    public class SkillActionState:State
    {
        public Timer action;

        public int id { get; set; }

        public override void Start()
        {
            action.Reset();
            character.animator.Attact(id);
        }
        public override void RunInternal()
        {
            action.Update(character.deltaTime.value);
        }
    }
}
