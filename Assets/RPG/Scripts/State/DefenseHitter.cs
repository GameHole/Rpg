using System;

namespace RPG
{
    public class DefenseHitter : IHitter
    {
        private Character cha;

        public DefenseHitter(Character cha)
        {
            this.cha = cha;
        }

        public IHitter hitter { get; private set; }

        public IRanger ranger { get; set; }

        public void Decorate()
        {
            this.hitter =cha.hitter;
        }

        public void Hit(HitInfo info)
        {
            if (ranger.isInRange(info.hitPoint))
            {
                cha.animator.DefenseHit();
                cha.strength -= info.demage;
                if (cha.strength < 0)
                    cha.strength = 0;
            }
            else
            {
                hitter.Hit(info);
            }
        }
    }
}