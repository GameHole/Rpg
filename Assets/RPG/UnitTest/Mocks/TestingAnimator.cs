using RPG;

namespace UnitTest
{
    internal class TestingAnimator : AAnimator
    {
        internal string log;

        public override void Idle()
        {
            log += "idle";
        }

        public override void Move()
        {
            log += "move";
        }

        public override void Attact(int id)
        {
            log += $"atk{id}";
        }

        public override void Hit()
        {
            log += "hit";
        }

        public override void Dead()
        {
            log += "dead";
        }
    }
}