using NUnit.Framework;
using RPG;

namespace UnitTest
{
    internal class TestSkill
    {
        private Timer[] acts;
        private SkillState skill;
        private TestingActionInput input;
        private TestingAnimator anim;
        private Character cha;

        [SetUp]
        public void set()
        {
            input = new TestingActionInput();
            anim = new TestingAnimator();
            cha = new Character(input, anim, new TestDeltaTime { _value=0.5f});
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
        public void testSkillActionState()
        {
            anim.log = null;
            var state = new SkillActionState();
            state.id = 1;
            state.SetCharacter(cha);
            var timer = new Timer();
            state.action = timer;
            state.RunInternal();
            Assert.AreEqual(0.5f, timer.runTime);
            state.Start();
            Assert.AreEqual(0, timer.runTime);
            Assert.AreEqual("atk1", anim.log);
        }
        [Test]
        public void testHit()
        {
            Assert.AreSame(cha.hitter, cha.defaultHitter);
            var hitter = new LogHitter();
            cha.hitter = hitter;
            cha.Hit(1);
            Assert.AreEqual("hit1", hitter.log);
        }
        [Test]
        public void testHitter()
        {
            Assert.IsFalse(cha.hittable.value);
            cha.hp = 10;
            var hitter = new Hitter(cha);
            hitter.Hit(1);
            Assert.AreEqual(9, cha.hp);
            Assert.IsTrue(cha.hittable.value);
        }
        [Test]
        public void testNoneHitter()
        {
            var hitter = new NoneHitter();
            Assert.DoesNotThrow(() => hitter.Hit(1));
        }
        [Test]
        public void testSkillHit()
        {
            var state = new SkillActionState();
            state.SetCharacter(cha);
            var timer = new Timer() { duration = 2 };
            state.action = timer;
            var filter = new TestingTargetFilter();
            state.hitTime = 1;
            state.targetFilter = filter;
            for (int c = 0; c < 2; c++)
            {
                cha.attact = 10;
                filter.target.hp = 10;
                filter.target.defense = 1;
                filter.target.log = null;
                state.Start();
                state.RunInternal();
                Assert.IsNull(filter.target.log);
                for (int i = 0; i < 4; i++)
                {
                    state.RunInternal();
                    Assert.AreEqual("hit9", filter.target.log);
                }
            }
           
        }
        [Test]
        public void testTransitionNextAction()
        {
            var mat = new StateMatchine();
            var timer = new Timer { duration = 1 };
            var tran = new TransitionToNextAction(1, timer);
            var log = new LogState();
            mat.SetState(1.ToEnum(), log);
            tran.SetCharacter(cha);
            tran.SetMatchine(mat);
            Assert.AreEqual(1.ToEnum(), tran.stateName);
            for (int i = 0; i < 2; i++)
            {
                input.isAttact = i == 0;
                for (int j = 0; j < 2; j++)
                {
                    timer.Update(0.5f);
                    Assert.AreEqual(i == 0 && j == 1, tran.isVailed());
                }
            }
            tran.Switch();
            Assert.AreEqual("start ",log.log);
        }
        [Test]
        public void testSkillStates()
        {
            var mat = skill.matchine;
            for (int i = 0; i < acts.Length; i++)
            {
                var state = mat.GetState<SkillActionState>(i.ToEnum());
                Assert.AreSame(acts[i], state.action);
                Assert.AreEqual(i, state.id);
                Assert.AreSame(cha, state.character);
                Assert.AreEqual(1, state.transations.Count);
                var tran = (state.transations[0] as TransitionToNextAction);
                Assert.AreEqual(((i + 1) % acts.Length).ToEnum(), tran.stateName);
                Assert.AreSame(acts[i], tran.finisher);
            }
            Assert.AreSame(mat.runingState, mat.GetState(0.ToEnum()));
            skill.RunInternal();
            Assert.AreEqual(0.5f,acts[0].runTime);
            skill.Start();
            Assert.AreNotSame(mat, skill.matchine);
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
