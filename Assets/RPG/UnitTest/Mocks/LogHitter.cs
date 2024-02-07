using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class LogHitter : Hitter
    {
        public string log;
        public LogHitter() : base(null)
        {
        }
        public override void Hit(int v)
        {
            log += $"hit{v}";
        }
    }
}
