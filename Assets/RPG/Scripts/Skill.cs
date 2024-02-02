using System.Collections.Generic;

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
        public float duration;
        public float runTime;
        public Skill()
        {
            transations.Add(new ToBasics { skill = this });
        }
        public override void Start()
        {
            character.animator.Attact();
            runTime = 0;
        }
        public override void Run()
        {
            runTime += character.deltaTime.value;
            base.Run();
        }
    }
}
