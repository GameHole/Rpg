using RPG;
using System.Collections.Generic;

namespace UnitTest
{
    internal class TestingTargetFilter : TargetFilter
    {
        internal LogCharacter target = new LogCharacter();
        public override IEnumerable<Character> FindTargets()
        {
            return new Character[] { target };
        }
    }
}