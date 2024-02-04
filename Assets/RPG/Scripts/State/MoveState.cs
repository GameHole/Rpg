using System.Collections.Generic;

namespace RPG
{
    public class MoveState : State
    {
        protected override void RunInternal()
        {
            character.position += character.input.moveDir * character.deltaTime.value;
        }
        public override void Start()
        {
            character.animator.Move();
        }
    }
}
