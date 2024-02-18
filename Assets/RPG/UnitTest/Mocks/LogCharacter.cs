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
        internal string log { get => loghitter.log; set => loghitter.log = value; }
        private LogHitter loghitter = new LogHitter();
        public LogCharacter() : base(null, null, null)
        {
            hitter = loghitter;
        }
    }
}
