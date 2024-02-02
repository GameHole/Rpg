﻿using UnityEngine;

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
        private Character character;
        private ToIdles toIdles = new ToIdles();
        private ToSkills toSkills = new ToSkills();
        public Move()
        {
            toSkills._this = this;
        }
        public void SetCharacter(Character character)
        {
            this.character = character;
            toIdles.SetCharacter(character);
            toSkills.SetCharacter(character);
        }
        public override void Run()
        {
            var dir = character.input.moveDir;
            character.position += dir;
            if (toSkills.isVailed())
            {
                toSkills.Switch();
                return;
            }
            if (toIdles.isVailed())
            {
                toIdles.Switch();
            }

        }

        public override void Start()
        {
            character.animator.Move();
        }
    }
}