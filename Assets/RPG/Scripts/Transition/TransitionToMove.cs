using System;

namespace RPG
{
    public class TransitionToMove : Transition
    {
        public override bool isVailed()
        {
            return character.input.moveDir != UnityEngine.Vector2.zero;
        }
        public override Enum stateName => StateName.Move;
    }
}
