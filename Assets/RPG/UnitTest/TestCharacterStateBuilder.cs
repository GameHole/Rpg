using NUnit.Framework;
using RPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    internal class TestCharacterStateBuilder: StateTesting
    {
        private StateMatchine matchine;
        public override void set()
        {
            base.set();
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
        class TestStateTransition
        {
            public ITransitionAssert[] assertions;
            public StateName name;
            public TestStateTransition(StateName name,ITransitionAssert[] assertions)
            {
                this.name = name;
                this.assertions = assertions;
            }
        }
        [Test]
        public void testTransitions()
        {
            var tests = new TestStateTransition[]
            {
                new TestStateTransition(StateName.Idle, new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToDead)),
                    new AssertType(typeof(TransitionToHit)),
                    new AssertType(typeof(TransitionToDefense)),
                    new AssertType(typeof(TransitionToSkill)),
                    new AssertType(typeof(TransitionToMove))
                }),
                new TestStateTransition(StateName.Move, new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToDead)),
                    new AssertType(typeof(TransitionToDefense)),
                    new AssertType(typeof(TransitionToSkill)),
                    new AssertType(typeof(TransitionToIdle))
                }),
                new TestStateTransition(StateName.Skill,new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToDead)),
                    new AssertType(typeof(TransitionToHit)),
                    new AssertBlocker(),
                    new AssertType(typeof(TransitionToDefense)),
                    new AssertType(typeof(TransitionToMove)),
                    new AssertType(typeof(TransitionToIdle))
                }),
                new TestStateTransition(StateName.Hit,new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToDead)),
                    new AssertType(typeof(TransitionToHit)),
                    new AssertFinisher()
                }),
                new TestStateTransition(StateName.Dead,new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToRevive))
                }),
                new TestStateTransition(StateName.Revive,new ITransitionAssert[]
                {
                    new AssertFinisher()
                }),
                new TestStateTransition(StateName.Defense,new ITransitionAssert[]
                {
                    new AssertType(typeof(TransitionToBreakDefense)),
                    new AssertType(typeof(TransitionToSkill)),
                    new AssertType(typeof(TransitionDefenseToIdle))
                }),
                new TestStateTransition(StateName.BreakDefense,new ITransitionAssert[]
                {
                    new AssertType( typeof(TransitionToDead)),
                    new AssertFinisher()
                })
            };
            foreach (var data in tests)
            {
                var msg = data.name.ToString();
                var taker = new TransitionTaker(matchine.GetState(data.name));
                Assert.AreEqual(data.assertions.Length, taker.TransitionCount, msg);
                foreach (var item in data.assertions)
                {
                    item.Assertion(taker, msg);
                }
            }
        }
        class AssertType: ITransitionAssert
        {
            Type type;
            public AssertType(Type type)
            {
                this.type = type;
            }
            public void Assertion(TransitionTaker taker, string msg)
            {
                Assert.AreEqual(type, taker.NextType(), msg);
            }
        }
        interface ITransitionAssert
        {
            void Assertion(TransitionTaker taker,string msg);
        }
        class AssertBlocker: ITransitionAssert
        {
            public void Assertion(TransitionTaker taker, string msg)
            {
                var finish = taker.Next<FinishTransitionBlocker>();
                Assert.AreSame(taker.state, finish.finisher, msg);
            }
        }
        class AssertFinisher : ITransitionAssert
        {
            public void Assertion(TransitionTaker taker, string msg)
            {
                var finish = taker.Next<FinishTransition>();
                Assert.AreSame((taker.state as AWaitingState).timer, finish.finisher);
                Assert.AreEqual(StateName.Idle, finish.stateName);
            }
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
