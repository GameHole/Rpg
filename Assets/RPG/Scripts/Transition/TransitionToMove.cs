using System;

namespace RPG
{
    public class TransitionToMove : Transition
    {
        public override bool isVailed()
        {
            return character.input.moveDir != UnityEngine.Vector2.zero;
        }
        protected override Enum stateName => StateName.Move;
    }
}
