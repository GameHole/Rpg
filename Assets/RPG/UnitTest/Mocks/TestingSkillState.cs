using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestingSkillState:SkillState
    {
        public bool _isFin;

        public override bool isFinish()
        {
            return _isFin;
        }
    }
}
