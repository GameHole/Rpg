using System;

namespace RPG
{
    public class TransitionToRevive : Transition
    {
        public override Enum stateName => StateName.Revive;

        public override bool isVailed()
        {
            return character.hp > 0;
        }
    }
}
