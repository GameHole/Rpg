using System;
using System.Collections.Generic;

namespace RPG
{
    public class IdleState: State
    {
        public IdleState()
        {
            transations.Add(new TransitionToSkill { _this = this });
            transations.Add(new TransitionToMove());
        }
        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
