using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public abstract class Transation
    {
        protected Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public abstract bool isVailed();
        public virtual void Switch()
        {
            character.SwitchTo(getWitchToAction());
        }
        protected abstract AAction getWitchToAction();
    }
    public class ToIdles: Transation
    {
        public override bool isVailed()
        {
            return character.input.moveDir == Vector2.zero;
        }
        protected override AAction getWitchToAction() => character.idle;
    }
    public class ToSkills: Transation
    {
        public AAction _this;
        public override bool isVailed()
        {
            return character.input.isAttact;
        }
        public override void Switch()
        {
            base.Switch();
            character.basicAction = _this;
        }

        protected override AAction getWitchToAction() => character.skill;
    }
    public class Move : AAction
    {
        public Move()
        {
            transations.Add(new ToSkills { _this = this });
            transations.Add(new ToIdles());
        }
        public override void Run()
        {
            var dir = character.input.moveDir;
            character.position += dir;
            base.Run();
        }

        public override void Start()
        {
            character.animator.Move();
        }
    }
}
