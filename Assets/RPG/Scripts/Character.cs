using UnityEngine;

namespace RPG
{
    public class Character 
    {
        public IInput input { get; }
        public AAnimator animator { get; }
        public DeltaTime deltaTime { get; }
        public IdleState idle { get; }
        public MoveState move { get; }
        public SkillState skill { get; }
        public State runingAction { get; set; }
        public Vector2 position { get; set; }
        public State basicAction { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;
            idle = new IdleState();
            move = new MoveState();
            skill = new SkillState();
            idle.SetCharacter(this);
            move.SetCharacter(this);
            skill.SetCharacter(this);
            SwitchTo(idle);
        }
        public void SwitchTo(State idle)
        {
            idle.Start();
            idle.Run();
            runingAction = idle;
        }
        public void Update()
        {
            runingAction.Run();
        }
    }
}

