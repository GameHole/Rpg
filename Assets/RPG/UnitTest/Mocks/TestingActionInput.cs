﻿using RPG;
using UnityEngine;

namespace UnitTest
{
    internal class TestingActionInput:IInput
    {
        public Vector2 moveDir { get; set; }
        public bool isAttact { get; set; }
        public bool isDefense { get; set; }
    }
}