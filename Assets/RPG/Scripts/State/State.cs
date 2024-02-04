﻿using System;
using System.Collections.Generic;

namespace RPG
{
    public class State
    {
        public Character character { get; private set; }
        public List<Transition> transations { get; } = new List<Transition>();
        public void SetCharacter(Character character)
        {
            this.character = character;
            foreach (var item in transations)
            {
                item.SetCharacter(character);
            }
        }
        public virtual void Start() { }
        public virtual void Run()
        {
            RunInternal();
            Transition();
        }

        protected virtual void Transition()
        {
            foreach (var item in transations)
            {
                if (item.isVailed())
                {
                    item.Switch();
                    return;
                }
            }
        }

        protected virtual void RunInternal() { }
    }
}