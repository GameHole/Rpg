using System;

namespace RPG
{
    public class ToMoves
    {
        private Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public bool isMove()
        {
            return character.input.moveDir != UnityEngine.Vector2.zero;
        }
        public void ToMove()
        {
            character.SwitchTo(character.move);
        }
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
            if (toSkills.isAttact())
            {
                toSkills.ToSkill();
                return;
            }
            if (toMoves.isMove())
            {
                toMoves.ToMove();
            }
        }
        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
