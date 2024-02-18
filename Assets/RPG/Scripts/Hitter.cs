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

        public virtual void Hit(HitInfo info)
        {
            cha.hittable.Set();
            cha.hp -= info.demage;
        }
    }
    public class NoneHitter : Hitter
    {
        public NoneHitter() : base(null)
        {
        }
        public override void Hit(HitInfo v) { }
    }
}