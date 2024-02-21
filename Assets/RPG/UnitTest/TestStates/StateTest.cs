using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.StateTest
{
    internal class StateTest<T>:StateTesting where T:State,new()
    {
        public T state;

        public override void set()
        {
            base.set();
            anim.log = null;
            state = new T();
            state.SetCharacter(cha);
        }        
    }
}
