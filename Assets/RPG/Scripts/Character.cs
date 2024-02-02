using UnityEngine;

namespace RPG
{
    public class Character 
    {
        public IInput input { get; }
        public AAnimator animator { get; }
        public DeltaTime deltaTime { get; }
        public Idle idle { get; }
        public Move move { get; }
        public Skill skill { get; }
        public AAction runingAction { get; set; }
        public Vector2 position { get; set; }
        public AAction basicAction { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;
            idle = new Idle(this);
            move = new Move(this);
            skill = new Skill(this);
            SwitchTo(idle);
        }
        public void SwitchTo(AAction idle)
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

