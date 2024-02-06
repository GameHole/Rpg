using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class LogCharacter : Character
    {
        internal string log;

        public LogCharacter() : base(null, null, null)
        {
        }


        public override void Hit(int v)
        {
            log += $"hit{v}";
        }
    }
}
