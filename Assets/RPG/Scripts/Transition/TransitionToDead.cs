using System;

namespace RPG
{
    public class TransitionToDead : Transition
    {
        public override Enum stateName =>StateName.Dead;

        public override bool isVailed()
        {
            return character.hp <= 0;
        }
    }
}
