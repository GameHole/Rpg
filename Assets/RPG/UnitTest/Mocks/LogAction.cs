using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class LogAction : State
    {
        public string log;
        public override void Run()
        {
            log += "run";
        }
    }
}
