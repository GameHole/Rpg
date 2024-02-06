namespace RPG
{
    public abstract class AAnimator
    {
        public abstract void Idle();
        public abstract void Move();
        public abstract void Attact(int id);
        public abstract void Hit();
        public abstract void Dead();
    }
}
