using UnityEngine;

namespace RPG
{
    public class Move : AAction
    {
        private Character character;

        public Move(Character character)
        {
            this.character = character;
        }
        public override void Run()
        {
            var dir = character.input.moveDir;
            character.position += dir;
            if (character.input.isAttact)
            {
                character.SwitchTo(character.skill);
                character.basicAction = this;
                return;
            }
            if (dir == Vector2.zero)
            {
                character.SwitchTo(character.idle);
            }
           
        }

        public override void Start()
        {
            character.animator.Move();
        }
    }
}
