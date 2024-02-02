using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnitTest
{
    internal static class AssertEx
    {
        internal static void AreEqualVec2(Vector2 exp, Vector2 act, float delta = 1e-6f)
        {
            Assert.AreEqual(exp.x, act.x, delta);
            Assert.AreEqual(exp.y, act.y, delta);
        }

        internal static void AreEqualVec3(Vector3 exp, Vector3 act, float delta = 1e-6f)
        {
            Assert.AreEqual(exp.x, act.x, delta);
            Assert.AreEqual(exp.y, act.y, delta);
            Assert.AreEqual(exp.z, act.z, delta);
        }

        internal static void AreNotEqualVec3(Vector3 exp, Vector3 act)
        {
            Assert.IsTrue(exp.x != act.x || exp.y != act.y || exp.z != act.z);
        }
    }
}
