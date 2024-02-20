using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestCharacterStateBuilder
    {
        private StateMatchine matchine;
        private Character cha;

        [SetUp]
        public void set()
        {
            cha = new Character(new TestingActionInput(),new TestingAnimator(), new TestDeltaTime());
            var builder = new CharacterStateBuilder();
            builder.Build(cha);
            matchine = cha.matchine;
        }
        [Test]
        public void testBasic()
        {
            var idle = new TransitionTaker(matchine.GetState<IdleState>(StateName.Idle));
            Assert.AreSame(cha, idle.state.character);
            Assert.AreSame(matchine, idle.Next().matchine);
        }
        [Test]
        public void testIdle()
        {
            var idle = new TransitionTaker(matchine.GetState<IdleState>(StateName.Idle));
            Assert.AreEqual(5, idle.TransitionCount);
            Assert.AreEqual(typeof(TransitionToDead), idle.NextType());
            Assert.AreEqual(typeof(TransitionToHit), idle.NextType());
            Assert.AreEqual(typeof(TransitionToDefense), idle.NextType());
            Assert.AreSame(typeof(TransitionToSkill), idle.NextType());
            Assert.AreEqual(typeof(TransitionToMove), idle.NextType());
        }
        [Test]
        public void testMove()
        {
            var move = new TransitionTaker(matchine.GetState<MoveState>(StateName.Move));
            Assert.AreEqual(4, move.TransitionCount);
            Assert.AreEqual(typeof(TransitionToDead), move.NextType());
            Assert.AreEqual(typeof(TransitionToDefense), move.NextType());
            Assert.AreSame(typeof(TransitionToSkill), move.NextType());
            Assert.AreEqual(typeof(TransitionToIdle), move.NextType());
        }
        [Test]
        public void testSkill()
        {
            var skill = new TransitionTaker(matchine.GetState<SkillState>(StateName.Skill));
            Assert.AreEqual(6, skill.TransitionCount);
            Assert.AreEqual(typeof(TransitionToDead), skill.NextType());
            Assert.AreEqual(typeof(TransitionToHit), skill.NextType());
            var finish = skill.Next<FinishTransitionBlocker>();
            Assert.AreSame(skill.state, finish.finisher);
            Assert.AreEqual(typeof(TransitionToDefense), skill.NextType());
            Assert.AreEqual(typeof(TransitionToMove), skill.NextType());
            Assert.AreEqual(typeof(TransitionToIdle), skill.NextType());
            
        }
        [Test]
        public void testHit()
        {
            var state = matchine.GetState<HitState>(StateName.Hit);
            var hit = new TransitionTaker(state);
            Assert.AreEqual(3, hit.TransitionCount);
            Assert.AreEqual(typeof(TransitionToDead), hit.NextType());
            Assert.AreEqual(typeof(TransitionToHit), hit.NextType());
            var finish = hit.Next<FinishTransition>();
            Assert.AreSame(state.timer, finish.finisher);
            Assert.AreEqual(StateName.Idle, finish.stateName);
        }
        [Test]
        public void testDead()
        {
            var dead = new TransitionTaker(matchine.GetState<DeadState>(StateName.Dead));
            Assert.AreEqual(1, dead.TransitionCount);
            Assert.AreEqual(typeof(TransitionToRevive), dead.NextType());
        }
        [Test]
        public void testRevive()
        {
            var state = matchine.GetState<ReviveState>(StateName.Revive);
            var dead = new TransitionTaker(state);
            Assert.AreEqual(1, dead.TransitionCount);
            var finish = dead.Next<FinishTransition>();
            Assert.AreSame(state.timer, finish.finisher);
            Assert.AreEqual(StateName.Idle, finish.stateName);
        }
        [Test]
        public void testDefense()
        {
            var dead = new TransitionTaker(matchine.GetState<DefenseState>(StateName.Defense));
            Assert.AreEqual(3, dead.TransitionCount);
            Assert.AreEqual(typeof(TransitionToBreakDefense), dead.NextType());
            Assert.AreEqual(typeof(TransitionToSkill), dead.NextType());
            Assert.AreEqual(typeof(TransitionDefenseToIdle), dead.NextType());
        }
        [Test]
        public void testBreakDefense()
        {
            var state = matchine.GetState<BreakDefenseState>(StateName.BreakDefense);
            var dead = new TransitionTaker(state);
            Assert.AreEqual(2, dead.TransitionCount);
            Assert.AreEqual(typeof(TransitionToDead), dead.NextType());
            var finish = dead.Next<FinishTransition>();
            Assert.AreSame(state.timer, finish.finisher);
            Assert.AreEqual(StateName.Idle, finish.stateName);
        }
        class TransitionTaker
        {
            public State state { get; private set; }
            int index = 0;
            public TransitionTaker(State state)
            {
                this.state = state;
            }

            public int TransitionCount => state.transations.Count;

            public Transition Next()
            {
                return state.transations[index++];
            }

            public T Next<T>() where T:Transition => Next() as T;

            public Type NextType() => Next().GetType();
        }
    }
}
