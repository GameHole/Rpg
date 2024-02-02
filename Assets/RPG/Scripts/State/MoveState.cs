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
        public override void Run()
        {
            var dir = character.input.moveDir;
            character.position += dir;
            base.Run();
        }

        public override void Start()
        {
            character.animator.Move();
        }
    }
}
