using System;

namespace RPG
{
    public class TransitionToDefense : Transition
    {
        public override Enum stateName =>StateName.Defense;

        public override bool isVailed()
        {
            return character.input.isDefense;
        }
    }
}
