﻿namespace RPG
{
    public abstract class AAnimator
    {
        public abstract void Idle();
        public abstract void Move();
        public abstract void Attact(int id);
        public abstract void Hit();
        public abstract void Dead();
        public abstract void Revive();
        public abstract void Defense();
        public abstract void DefenseHit();
        public abstract void BreakDefense();
        public abstract void Down();
    }
}
