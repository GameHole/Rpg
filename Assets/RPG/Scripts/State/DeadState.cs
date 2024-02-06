namespace RPG
{
    public class DeadState : State
    {
        public override void Start()
        {
            character.animator.Dead();
        }
    }
}
