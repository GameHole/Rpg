using System.Collections.Generic;

namespace RPG
{
    public class SkillState : State
    {
        public float duration;
        public float runTime;
        public SkillState()
        {
            transations.Add(new TransitionToBasic { skill = this });
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
