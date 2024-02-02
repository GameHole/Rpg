using UnityEngine;

namespace RPG
{
    public class ToIdles
    {
        private Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public bool isStop()
        {
            return character.input.moveDir == Vector2.zero;
        }
        public void ToIdle()
        {
            character.SwitchTo(character.idle);
        }
    }
    public class ToSkills
    {
        public AAction _this;
        private Character character;

        public void SetCharacter(Character character)
        {
            this.character = character;
        }
        public bool isAttact()
        {
            return character.input.isAttact;
        }
        public void ToSkill()
        {
            character.SwitchTo(character.skill);
            character.basicAction = _this;
        }
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
            if (toSkills.isAttact())
            {
                toSkills.ToSkill();
                return;
            }
            if (toIdles.isStop())
            {
                toIdles.ToIdle();
            }

        }

        public override void Start()
        {
            character.animator.Move();
        }
    }
}
