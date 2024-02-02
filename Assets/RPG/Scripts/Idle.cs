using System;

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
        private Character character;
        private ToSkills toSkills = new ToSkills();
        private ToMoves toMoves = new ToMoves();
        public Idle()
        {
            toSkills._this = this;
        }
        public void SetCharacter(Character character)
        {
            this.character = character;
            toSkills.SetCharacter(character);
            toMoves.SetCharacter(character);
        }

        public override void Run()
        {
            if (toSkills.isVailed())
            {
                toSkills.Switch();
                return;
            }
            if (toMoves.isVailed())
            {
                toMoves.Switch();
            }
        }
        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
