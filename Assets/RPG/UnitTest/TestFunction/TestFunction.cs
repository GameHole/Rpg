using RPG;
namespace UnitTest
{
    class TestFunction : StateTesting
    {
        public StateMatchine matchine;
        public Timer[] acts;

        public override void set()
        {
            base.set();
            matchine = cha.matchine;
            var builder = new CharacterStateBuilder();
            builder.Build(cha);
            var state = cha.matchine.GetState<SkillState>(StateName.Skill);
            acts = new Timer[2];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new Timer() { duration = 2 };
                state.actions.Add(acts[i]);
            }
        }
    }
}

