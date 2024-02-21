using System;

namespace RPG
{
    public interface IHitter
    {
        void Hit(HitInfo info);
    }
    public class Hitter: IHitter
    {
        private Character cha;
        public Hitter() { }
        public Hitter(Character cha)
        {
            this.cha = cha;
        }

        public virtual void Hit(HitInfo info)
        {
            cha.hittable.Set();
            if (info.down)
            {
                cha.down.Set();
            }
            cha.hp -= info.demage;
        }
    }
    public class NoneHitter : IHitter
    {
        public void Hit(HitInfo v) { }
    }
}