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

        public override void Attact()
        {
            log += "atk";
        }

        public override void Hit()
        {
            log += "hit";
        }
    }
}