using System;

namespace RPG
{
    public class TransitionToHit : Transition
    {
        public override Enum stateName => StateName.Hit;

        public override bool isVailed()
        {
            bool ret = character.hittable.value;
            character.hittable.Reset();
            return ret;
        }
    }
}
