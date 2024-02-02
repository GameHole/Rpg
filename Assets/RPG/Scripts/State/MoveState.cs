using System.Collections.Generic;

namespace RPG
{
    public class MoveState : State
    {
        public MoveState()
        {
            transations.Add(new TransitionToSkill { _this = this });
            transations.Add(new TransitionToIdle());
        }
        protected override void RunInternal()
        {
            character.position += character.input.moveDir;
        }
        public override void Start()
        {
            character.animator.Move();
        }
    }
}
