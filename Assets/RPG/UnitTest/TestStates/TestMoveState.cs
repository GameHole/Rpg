using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnitTest.StateTest
{
    internal class TestMoveState:StateTest<MoveState>
    {
        [Test]
        public void test()
        {
            input.moveDir = new Vector2(0.1f, 0);
            state.Start();
            Assert.AreEqual(anim.Str("move"), anim.log);
            for (int i = 0; i < 2; i++)
            {
                state.RunInternal();
                AssertEx.AreEqualVec2(new Vector2(0.1f * 0.5f * (i + 1), 0), cha.position);
            }
        }
    }
}
