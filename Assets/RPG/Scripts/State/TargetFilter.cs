using System;
using System.Collections.Generic;

namespace RPG
{
    public class TargetFilter
    {
        public virtual IEnumerable<Character> FindTargets()
        {
            return new Character[0];
        }
    }
}