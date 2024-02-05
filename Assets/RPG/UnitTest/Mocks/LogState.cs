using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class LogState:State
    {
        internal string log;

        public override void Start()
        {
            log += "start ";
        }
        public override void RunInternal()
        {
            log += "run ";
        }
        protected override void Transition()
        {
            log += "transition ";
        }

        internal void TestTransition()
        {
            base.Transition();
        }
    }
}
