using UnityEngine;

namespace EnemyScripts.Interfaces
{
    public interface IEnemyMoveable
    {
        Rigidbody2D Rb { get; set; }
        bool IsFacingRight { get; set; }
        void MoveEnemy(Vector2 velocity);
        void CheckForLeftOrRightFacing(Vector2 velocity);
    }
}
