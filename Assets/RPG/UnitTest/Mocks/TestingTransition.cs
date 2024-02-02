using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestingTransition : Transition
    {
        public override bool isVailed()
        {
            throw new NotImplementedException();
        }

        protected override State getWitchToAction()
        {
            throw new NotImplementedException();
        }
    }
}
