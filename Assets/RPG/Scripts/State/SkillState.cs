using System.Collections.Generic;

namespace RPG
{
    public class SkillState : State
    {
        public float duration;
        public float runTime { get; private set; }
        public override void Start()
        {
            character.animator.Attact();
            runTime = 0;
        }
        protected override void RunInternal()
        {
            runTime += character.deltaTime.value;
        }
    }
}
