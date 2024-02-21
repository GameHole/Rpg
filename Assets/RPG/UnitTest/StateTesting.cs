using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class StateTesting
    {
        protected TestingAnimator anim;
        protected TestingActionInput input;
        protected TestDeltaTime deltaTime;
        protected Character cha;

        [SetUp]
        public virtual void set()
        {
            anim = new TestingAnimator();
            input = new TestingActionInput();
            deltaTime = new TestDeltaTime();
            cha = new Character(input, anim, deltaTime);
            deltaTime._value = 0.5f;
        }
    }
}
