using System;
using UnityEngine;

namespace RPG
{
    public enum StateName
    {
        Idle,Move,Skill,
        Hit,
        Dead
    }
    public class Character 
    {
        public IInput input { get; }
        public DeltaTime deltaTime { get; }
        public StateMatchine matchine { get; } = new StateMatchine();
        public AAnimator animator { get; }
        public Vector2 position { get; set; }
        public Hittable hittable { get; set; } = new Hittable();
        public int attact { get; set; }
        public int hp { get; set; } = 2;
        public int defense { get;set; }
        public Hitter hitter { get; set; }

        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;
            hitter = new Hitter(this);
        }
        public void Update()
        {
            matchine.Update();
        }

        public virtual void Hit(int v)
        {
            hitter.Hit(v);
        }
    }
}

