using NUnit.Framework;
using RPG;

namespace UnitTest
{
    internal class TestSkill
    {
        private Timer[] acts;
        private SkillState skill;
        private TestingActionInput input;

        [SetUp]
        public void set()
        {
            input = new TestingActionInput();
            var cha = new Character(input, new TestingAnimator(), new TestDeltaTime { _value=0.5f});
            skill = new SkillState();
            skill.SetCharacter(cha);
            acts = new Timer[2];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new Timer() { duration = 2 };
                skill.actions.Add(acts[i]);
            }
            skill.Start();
        }
        [Test]
        public void testSkillOnePass()
        {
            input.isAttact = false;
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(i * 0.5f, acts[0].runTime);
                Assert.IsFalse(skill.isFinish());
                Assert.AreEqual(0, acts[1].runTime);
                skill.RunInternal();
            }
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
            Assert.IsTrue(skill.isFinish());
        }
        [Test]
        public void testSkillTwoPass()
        {
            input.isAttact = true;
            for (int i = 0; i < 4; i++)
            {
                skill.RunInternal();
            }
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
            Assert.IsFalse(skill.isFinish());
            for (int i = 0; i < 3; i++)
            {
                skill.RunInternal();
            }
            input.isAttact = false;
            skill.RunInternal();
            Assert.AreEqual(2, acts[1].runTime);
            Assert.IsTrue(skill.isFinish());
        }
        [Test]
        public void testSkillLoop()
        {
            input.isAttact = true;
            for (int i = 0; i < 8; i++)
            {
                skill.RunInternal();
            }
            Assert.IsFalse(skill.isFinish());
            Assert.AreEqual(0, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
            skill.RunInternal();
            Assert.AreEqual(0.5f, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
            Assert.IsFalse(skill.isFinish());
        }
    }
}
