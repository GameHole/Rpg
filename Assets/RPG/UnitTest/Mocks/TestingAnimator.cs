using RPG;
using System;

namespace UnitTest
{
    internal class TestingAnimator : AAnimator
    {
        internal string log;

        public override void Idle()
        {
            AppendName("idle");
        }

        private void AppendName(string name)
        {
            log += Str(name);
        }

        public override void Move()
        {
            AppendName("move");
        }

        public override void Attact(int id)
        {
            AppendName($"atk{id}");
        }

        public override void Hit()
        {
            AppendName("hit");
        }

        public override void Dead()
        {
            AppendName("dead");
        }

        public override void Revive()
        {
            AppendName("revive");
        }

        public override void Defense()
        {
            AppendName("defense");
        }

        public override void DefenseHit()
        {
            AppendName("defenseHit");
        }

        public override void BreakDefense()
        {
            AppendName("breakdefense");
        }

        public override void Down()
        {
            AppendName("down");
        }

        internal string Str(string v)
        {
            return v + " ";
        }
    }
}