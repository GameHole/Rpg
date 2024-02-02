using UnityEngine;

namespace RPG
{
    public class Character 
    {
        public IInput input { get; }
        public AAnimator Anim { get; }
        public Idle idle { get; }
        public Move move { get; }
        
        public Character(IInput input, AAnimator anim)
        {
            this.input = input;
            Anim = anim;
            idle = new Idle(this);
            move = new Move(this);
            SwitchTo(idle);
        }

        public AAction runingAction { get; set; }
        public Vector2 position { get; set; }
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

