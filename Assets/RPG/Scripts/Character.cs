using System;
using UnityEngine;

namespace RPG
{
    public enum StateName
    {
        Idle,Move,Skill,
        Hit,
        Dead,
        Revive,
        Defense
    }
    public class HitInfo
    {
        public int demage;
        public Vector3 hitPoint;
    }
    public class Character 
    {
        public IInput input { get; }
        public DeltaTime deltaTime { get; }
        public StateMatchine matchine { get; } = new StateMatchine();
        public AAnimator animator { get; }
        public Hittable hittable { get; set; } = new Hittable();
        public IHitter hitter { get; set; }
        public Hitter defaultHitter { get; }

        public Vector2 position { get; set; }
        public int attact { get; set; }
        public int hp { get; set; } = 2;
        public int defense { get;set; }
        public int strength { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;
            defaultHitter = new Hitter(this);
            ResetHitter();
        }

        public void ResetHitter()
        {
            hitter = defaultHitter;
        }

        public void Update()
        {
            matchine.Update();
        }

        public virtual void Hit(HitInfo info)
        {
            hitter.Hit(info);
        }
    }
}

