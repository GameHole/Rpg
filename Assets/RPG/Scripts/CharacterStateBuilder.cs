using System;

namespace RPG
{
    public class CharacterStateBuilder
    {
        public void Build(Character cha)
        {
            var matchine = cha.matchine;
            var idle = new IdleState();
            idle.transations.Add(new TransitionToHit());
            idle.transations.Add(new TransitionToSkill { _this = idle });
            idle.transations.Add(new TransitionToMove());
            var move = new MoveState();
            move.transations.Add(new TransitionToSkill { _this = move });
            var toIdle = new TransitionToIdle();
            move.transations.Add(toIdle);
            var skill = new SkillState();
            skill.transations.Add(new FinishTransition(StateName.Basic, skill));
            var hit = new HitState();
            hit.transations.Add(new TransitionToHit());
            hit.transations.Add(new FinishTransition(StateName.Idle, hit.timer));
            matchine.SetState(StateName.Idle, idle);
            matchine.SetState(StateName.Move, move);
            matchine.SetState(StateName.Skill, skill);
            matchine.SetState(StateName.Hit, hit);
            matchine.SetCharacter(cha);
            toIdle.Switch();
        }
    }
}
