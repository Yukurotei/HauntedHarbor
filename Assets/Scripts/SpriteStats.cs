using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BY
{
    [CreateAssetMenu(menuName = "Hh/SpriteStats")]
    public class SpriteStats : ScriptableObject
    {
        [Header("Generic")]
        public float maxHealth;
        public SpriteType spriteType;
        [Header("Attack")]
        public float fireRate;
        public AttackType attackType;
        public float fireOffset;
        public GameObject bullet;
        [Header("Movement")]
        public float speed;
        public float jumpForce;
        public MoveType moveType;
        public float moveRange;
        public float moveRadius;
        [Header("Audio")]
        public AudioClip shoot;
        public AudioClip hurt;
        public AudioClip death;
    }
}