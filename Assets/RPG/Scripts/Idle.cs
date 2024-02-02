using System;
using System.Collections.Generic;

namespace RPG
{
    public class ToMoves : Transation
    {
        public override bool isVailed()
        {
            return character.input.moveDir != UnityEngine.Vector2.zero;
        }
        protected override AAction getWitchToAction() => character.move;
    }
    public class Idle: AAction
    {
        public Idle()
        {
            transations.Add(new ToSkills { _this = this });
            transations.Add(new ToMoves());
        }
        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
