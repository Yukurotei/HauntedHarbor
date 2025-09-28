using UnityEngine;

namespace BY
{
    public class EnemyController : Sprite
    {
        public LayerMask LayerMask;
        public Vector2 spriteSize;
        public Transform target;
        public AttackPattern attack;
        public MovePattern move;
        public float radius;
        public float angle;
        public Vector3 spawnLocation;
        public float range;

        void Start()
        {
            currentHealth = stats.maxHealth;
            attack = AttackPatterns.KeepShooting;
            move = MovePatterns.MoveOnPlatform;
            animator = GetComponentInChildren<Animator>();
            spriteSize = animator.transform.localScale;
            rb = GetComponent<Rigidbody2D>();
            currentSpeed = stats.speed;
            currentAttackType = stats.attackType;
            currentMoveType = stats.moveType;
            range = stats.moveRange;
            radius = stats.moveRadius;
            spawnLocation = transform.position;
            selectAttackType();
            selectMovementType();
        }

        void Update()
        {
            handleAnimations();
            if (currentHealth <= 0)
            {
                rb.velocity = Vector3.zero;
                if (stats.spriteType == SpriteType.Airborne)
                {
                    Destroy(GetComponentInChildren<CircleCollider2D>());
                }
                return;
            }
            handleMovment();
            handleShooting();
        }

        public override void handleMovment()
        {
            move?.Invoke(this);
            //Raw Movement
            if (stats.spriteType == SpriteType.Grounded)
            {
                yVelocity += gravity * Time.deltaTime;
                if (groundCheck.isGrounded)
                {
                    yVelocity = 0f;
                    if (movement.y > 0)
                    {
                        yVelocity = stats.jumpForce;
                    }
                }
            }
            movement.y = yVelocity;

            //Calculated movement(Final)
            rb.velocity = movement * currentSpeed;
        }

        protected void selectMovementType()
        {
            if (movement.x == 0 && direction == 0)
            {
                while (direction == 0)
                {
                    direction = Random.Range(-1, 2); //equivlent to -1, 1 since max value is exclusive
                }
            }

            switch (currentMoveType)
            {
                default:
                    move = null;
                    break;
                case MoveType.None:
                    move = null;
                    break;
                case MoveType.Normal:
                    move = MovePatterns.MoveOnPlatform;
                    break;
                case MoveType.Circle:
                    move = MovePatterns.CircleAroundPoint;
                    break;
                case MoveType.Jumping:
                    move = MovePatterns.Jumping;
                    break;
            }
        }

        protected void selectAttackType()
        {
            switch (currentAttackType) {
                default:
                    target = null;
                    attack = null;
                    break;
                case AttackType.None:

                    break;
                case AttackType.Constant:
                    attack = AttackPatterns.KeepShooting;
                    break;
                case AttackType.Target:
                    target = PLAYER_INSTANCE.transform;
                    attack = AttackPatterns.TargetEntity;
                    break;
            }
        }

        public override void handleShooting()
        {
            attack?.Invoke(this);
        }

        protected override void handleAnimations()
        {
            animator.SetFloat("MovementX", Mathf.Abs(movement.x));
            if (stats.spriteType == SpriteType.Grounded)
            {
                animator.SetBool("IsGrounded", groundCheck.isGrounded);
            }
            animator.SetBool("IsShooting", shootTimer < 0.2);
            animator.SetBool("IsAlive", currentHealth > 0);
            if (movement.x != 0)
            {
                direction = Mathf.Sign(movement.x);
            }
            animator.SetFloat("Direction", direction);
        }
    }

}