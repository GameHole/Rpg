using System;

namespace RPG
{
    public class TransitionToBreakDefense : Transition
    {
        public override Enum stateName => StateName.BreakDefense;
        private int strength = 0;
        public override bool isVailed()
        {
            if (strength != character.strength)
            {
                strength = character.strength;
                return strength == 0;
            }
            return false;
        }
    }
}
