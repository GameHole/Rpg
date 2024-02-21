using NUnit.Framework;
using RPG;

namespace UnitTest.StateTest
{
    internal class TestSkillState:StateTest<SkillState>
    {
        private Timer[] acts;

        [SetUp]
        public override void set()
        {
            base.set();
            acts = new Timer[2];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new Timer() { duration = 2 };
                state.actions.Add(acts[i]);
            }
            state.Start();
        }
        
        [Test]
        public void testHit()
        {
            Assert.AreSame(cha.hitter, cha.defaultHitter);
            var hitter = new LogHitter();
            cha.hitter = hitter;
            cha.Hit(new HitInfo { demage = 1 });
            Assert.AreEqual("hit1", hitter.log);
        }
        [Test]
        public void testHitter()
        {
            Assert.IsFalse(cha.hittable.value);
            cha.hp = 10;
            var hitter = new Hitter(cha);
            hitter.Hit(new HitInfo { demage = 1 });
            Assert.AreEqual(9, cha.hp);
            Assert.IsTrue(cha.hittable.value);
            Assert.IsFalse(cha.down.value);
        }
        [Test]
        public void testHitDown()
        {
            var hitter = new Hitter(cha);
            hitter.Hit(new HitInfo { demage = 1, down=true });
            Assert.IsTrue(cha.hittable.value);
            Assert.IsTrue(cha.down.value);
        }
        [Test]
        public void testNoneHitter()
        {
            var hitter = new NoneHitter();
            Assert.DoesNotThrow(() => hitter.Hit(new HitInfo { demage=1}));
        }
        [Test]
        public void testSkillStates()
        {
            var mat = state.matchine;
            for (int i = 0; i < acts.Length; i++)
            {
                var state = mat.GetState<SkillActionState>(i.ToEnum());
                Assert.AreSame(acts[i], state.timer);
                Assert.AreEqual(i, state.id);
                Assert.AreSame(cha, state.character);
                Assert.AreEqual(1, state.transations.Count);
                var tran = (state.transations[0] as TransitionToNextAction);
                Assert.AreEqual(((i + 1) % acts.Length).ToEnum(), tran.stateName);
                Assert.AreSame(acts[i], tran.finisher);
            }
            Assert.AreSame(mat.runingState, mat.GetState(0.ToEnum()));
            state.RunInternal();
            Assert.AreEqual(0.5f,acts[0].runTime);
            state.Start();
            Assert.AreNotSame(mat, state.matchine);
        }
        [Test]
        public void testSkillOnePass()
        {
            input.isAttact = false;
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(i * 0.5f, acts[0].runTime);
                Assert.IsFalse(state.isFinish());
                Assert.AreEqual(0, acts[1].runTime);
                state.RunInternal();
            }
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
            Assert.IsTrue(state.isFinish());
        }
        [Test]
        public void testSkillTwoPass()
        {
            input.isAttact = true;
            for (int i = 0; i < 4; i++)
            {
                state.RunInternal();
            }
            Assert.AreEqual(2, acts[0].runTime);
            Assert.AreEqual(0, acts[1].runTime);
            Assert.IsFalse(state.isFinish());
            for (int i = 0; i < 3; i++)
            {
                state.RunInternal();
            }
            input.isAttact = false;
            state.RunInternal();
            Assert.AreEqual(2, acts[1].runTime);
            Assert.IsTrue(state.isFinish());
        }
        [Test]
        public void testSkillLoop()
        {
            input.isAttact = true;
            for (int i = 0; i < 8; i++)
            {
                state.RunInternal();
            }
            Assert.IsFalse(state.isFinish());
            Assert.AreEqual(0, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
            state.RunInternal();
            Assert.AreEqual(0.5f, acts[0].runTime);
            Assert.AreEqual(2, acts[1].runTime);
            Assert.IsFalse(state.isFinish());
        }
    }
}
