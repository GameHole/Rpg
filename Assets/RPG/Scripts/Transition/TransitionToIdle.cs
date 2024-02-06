using System;
using UnityEngine;

namespace RPG
{
    public class TransitionToIdle: Transition
    {
        public override bool isVailed()
        {
            return character.input.moveDir == Vector2.zero;
        }
        public override Enum stateName => StateName.Idle;
    }
}
