﻿namespace RPG
{
    public class DefenseState : State
    {
        public DefenseHitter hitter { get; private set; }
        public override void SetCharacter(Character character)
        {
            base.SetCharacter(character);
            hitter= new DefenseHitter(character);
        }
        public override void Start()
        {
            character.animator.Defense();
            hitter.Decorate();
            character.hitter = hitter;
        }
        public override void End()
        {
            character.hitter = hitter.hitter;
        }
    }
}
