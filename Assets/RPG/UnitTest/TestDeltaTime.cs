using RPG;

namespace UnitTest
{
    internal class TestDeltaTime: DeltaTime
    {
        public float _value;

        public override float value => _value;
    }
}