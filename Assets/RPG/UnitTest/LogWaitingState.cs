using RPG;

namespace UnitTest
{
    internal class LogWaitingState: AWaitingState
    {
        internal string log;

        protected override void Play()
        {
            log += "play";
        }
    }
}