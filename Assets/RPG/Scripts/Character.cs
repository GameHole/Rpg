using System;
using UnityEngine;

namespace RPG
{
    public enum StateName
    {
        Idle,Move,Skill,Basic,
        Hit
    }
    public class Character 
    {
        public IInput input { get; }
        public DeltaTime deltaTime { get; }
        public StateMatchine matchine { get; } = new StateMatchine();
        public AAnimator animator { get; }
        public Vector2 position { get; set; }
        public Hittable hittable { get; set; } = new Hittable();
        public Character(IInput input, AAnimator anim, DeltaTime deltaTime)
        {
            this.input = input;
            animator = anim;
            this.deltaTime = deltaTime;
        }
        public void Update()
        {
            matchine.Update();
        }

        public void Hit(int v)
        {
            hittable.Set();
        }
    }
}

