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

        [SetUp]
        public void set()
        {
            var cha = new Character(new TestingActionInput(),new TestingAnimator(), new TestDeltaTime());
            var builder = new CharacterStateBuilder();
            builder.Build(cha);
            matchine = cha.matchine;
        }
        [Test]
        public void testIdle()
        {
            var idle = matchine.GetState<IdleState>(StateName.Idle);
            Assert.AreEqual(2, idle.transations.Count);
            Assert.AreSame(idle, (idle.transations[0] as TransitionToSkill)._this);
            Assert.AreEqual(typeof(TransitionToMove), idle.transations[1].GetType());
        }
        [Test]
        public void testMove()
        {
            var move = matchine.GetState<MoveState>(StateName.Move);
            Assert.AreEqual(2, move.transations.Count);
            Assert.AreSame(move, (move.transations[0] as TransitionToSkill)._this);
            Assert.AreEqual(typeof(TransitionToIdle), move.transations[1].GetType());
        }
        [Test]
        public void testSkill()
        {
            var skill = matchine.GetState<SkillState>(StateName.Skill);
            Assert.AreEqual(1, skill.transations.Count);
            Assert.AreSame(skill, (skill.transations[0] as TransitionToBasic).skill);
        }
    }
}
