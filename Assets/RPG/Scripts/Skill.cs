namespace RPG
{
    public class ToBasics : Transation
    {
        public Skill skill;
        public override bool isVailed()
        {
            return skill.runTime > skill.duration;
        }
        protected override AAction getWitchToAction() => character.basicAction;
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
            if (basics.isVailed())
            {
                basics.Switch();
            }
        }

       
    }
}
