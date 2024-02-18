using RPG;
using UnityEngine;

namespace UnitTest
{
    internal class TestingRanger : IRanger
    {
        public bool isInRange(Vector3 point)
        {
            return point == Vector3.zero;
        }
    }
}