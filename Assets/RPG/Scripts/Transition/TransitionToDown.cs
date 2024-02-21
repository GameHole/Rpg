using System;

namespace RPG
{
    public class TransitionToDown : Transition
    {
        public override Enum stateName => StateName.Down;

        public override bool isVailed()
        {
            return character.down.value;
        }
    }
}
