using System;

namespace RPG
{
    public class CharacterStateBuilder
    {
        public void Build(Character cha)
        {
            var matchine = cha.matchine;
            var idle = new IdleState();
            idle.transations.Add(new TransitionToDead());
            idle.transations.Add(new TransitionToHit());
            idle.transations.Add(new TransitionToSkill());
            idle.transations.Add(new TransitionToMove());
            var move = new MoveState();
            move.transations.Add(new TransitionToDead());
            move.transations.Add(new TransitionToSkill());
            var toIdle = new TransitionToIdle();
            move.transations.Add(toIdle);
            var skill = new SkillState();
            skill.transations.Add(new TransitionToDead());
            skill.transations.Add(new FinishTransitionBlocker(skill));
            skill.transations.Add(new TransitionToMove());
            skill.transations.Add(new TransitionToIdle());
            var hit = new HitState();
            hit.transations.Add(new TransitionToDead());
            hit.transations.Add(new TransitionToHit());
            hit.transations.Add(new FinishTransition(StateName.Idle, hit.timer));
            var dead = new DeadState();
            matchine.SetState(StateName.Idle, idle);
            matchine.SetState(StateName.Move, move);
            matchine.SetState(StateName.Skill, skill);
            matchine.SetState(StateName.Hit, hit);
            matchine.SetState(StateName.Dead, dead);
            matchine.SetCharacter(cha);
            toIdle.Switch();
        }
    }
}
