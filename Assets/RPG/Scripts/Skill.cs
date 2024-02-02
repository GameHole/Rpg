namespace RPG
{
    public class Skill : AAction
    {
        private Character character;
        public float duration;
        private float runTime;
        public Skill(Character character)
        {
            this.character = character;
        }
        public override void Start()
        {
            character.animator.Attact();
            runTime = 0;
        }
        public override void Run()
        {
            runTime += character.deltaTime.value;
            if (runTime > duration)
            {
                character.SwitchTo(character.basicAction);
            }
        }
    }
}
