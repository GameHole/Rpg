using UnityEngine;

namespace RPG
{
    public class Character 
    {
        public IInput input { get; }
        public AAnimator animator { get; }
        public DeltaTime deltaTime { get; }
        public State idle { get; set; }
        public State move { get; }
        public SkillState skill { get; }
        public State runingState { get; set; }
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
        public void SwitchTo(State state)
        {
            state.Start();
            state.Run();
            runingState = state;
        }
        public void Update()
        {
            runingState.Run();
        }
    }
}

