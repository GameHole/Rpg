using System;

namespace RPG
{
    public class Hitter
    {
        private Character cha;

        public Hitter(Character cha)
        {
            this.cha = cha;
        }

        public virtual void Hit(int v)
        {
            cha.hittable.Set();
            cha.hp -= v;
        }
    }
    public class NoneHitter : Hitter
    {
        public NoneHitter() : base(null)
        {
        }
        public override void Hit(int v) { }
    }
}