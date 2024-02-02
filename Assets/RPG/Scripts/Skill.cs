namespace RPG
{
    public class ToBasics
    {
        public Skill skill;
        private Character character;
        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public void ToBasic()
        {
            character.SwitchTo(character.basicAction);
        }


        public bool isFinish()
        {
            return skill.runTime > skill.duration;
        }
    }
    public class Skill : AAction
    {
        private Character character;
        private ToBasics basics = new ToBasics();
        public float duration;
        public float runTime;
        public Skill()
        {
            basics.skill = this;
        }
        public void SetCharacter(Character character)
        {
            this.character = character;
            basics.SetCharacter(character);
        }
        public override void Start()
        {
            character.animator.Attact();
            runTime = 0;
        }
        public override void Run()
        {
            runTime += character.deltaTime.value;
            if (basics.isFinish())
            {
                basics.ToBasic();
            }
        }

       
    }
}
