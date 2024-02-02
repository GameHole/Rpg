using System;

namespace RPG
{
    public class Idle: AAction
    {
        private Character character;

        public Idle(Character character)
        {
            this.character = character;
        }

        public override void Run()
        {
            if (character.input.isAttact)
            {
                character.SwitchTo(character.skill);
                character.basicAction = this;
                return;
            }
            if (character.input.moveDir != UnityEngine.Vector2.zero)
            {
                character.SwitchTo(character.move);
            }
        }

        public override void Start()
        {
            character.animator.Idle();
        }
    }
}
