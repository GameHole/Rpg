using System;
using System.Collections.Generic;

namespace RPG
{
    public class IdleState: State
    {
        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
