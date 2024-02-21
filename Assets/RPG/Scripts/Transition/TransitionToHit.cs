using System;

namespace RPG
{
    public class TransitionToHit : Transition
    {
        public override Enum stateName => StateName.Hit;

        public override bool isVailed()
        {
            return character.hittable.value;
        }
    }
}
