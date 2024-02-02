using UnityEngine;

namespace RPG
{
    public interface IInput
    {
        Vector2 moveDir { get; }
        bool isAttact { get; set; }
    }
}