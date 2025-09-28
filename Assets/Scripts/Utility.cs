using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BY
{
    public static class animationStates
    {
        public static int movementX = Animator.StringToHash("MovementX");
        public static int direction = Animator.StringToHash("Direction");
        public static int grounded = Animator.StringToHash("IsGrounded");
        public static int shooting = Animator.StringToHash("IsShooting");
        public static int alive = Animator.StringToHash("IsAlive");
    }

    public enum SpriteType
    {
        Grounded,
        Airborne
    }

    public enum AttackType
    {
        None,
        Constant,
        Target
    }

    public enum MoveType
    {
        None,
        Normal,
        Circle,
        Jumping
    }

    public delegate void AttackPattern(EnemyController ec);

    public delegate void MovePattern(EnemyController ec);
}