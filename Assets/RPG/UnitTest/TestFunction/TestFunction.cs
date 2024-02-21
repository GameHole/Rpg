using RPG;
namespace UnitTest
{
    class TestFunction : StateTesting
    {
        public StateMatchine matchine;
        public ActionClip[] acts;

        public override void set()
        {
            base.set();
            matchine = cha.matchine;
            var builder = new CharacterStateBuilder();
            builder.Build(cha);
            var state = cha.matchine.GetState<SkillState>(StateName.Skill);
            acts = new ActionClip[2];
            LoadClips(state, acts);
        }

        public static void LoadClips(SkillState state, ActionClip[] acts)
        {
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new ActionClip() { duration = 2 };
                state.actions.Add(acts[i]);
            }
        }
    }
}

