using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BY
{
    public class Sprite : MonoBehaviour
    {
        [Header("Stats")]
        public SpriteStats stats;
        [Header("States")]
        public float currentSpeed;
        public AttackType currentAttackType;
        public MoveType currentMoveType;
        public float currentHealth;
        [Header("Debug")]
        public Vector2 movement;
        public float yVelocity;
        public float gravity = -9.8f;
        public float direction = 1f;
        public bool isShooting;
        public InputController controller;
        public Rigidbody2D rb;
        public GroundCheck groundCheck;
        public float shootTimer;

        protected Animator animator;

        public static Sprite PLAYER_INSTANCE = null;

        void Awake()
        {
            if (tag == "Player")
            {
                PLAYER_INSTANCE = this;
            }
            controller = GetComponent<InputController>();
            rb = GetComponent<Rigidbody2D>();
            groundCheck = GetComponentInChildren<GroundCheck>();
            currentHealth = stats.maxHealth;
            currentSpeed = stats.speed;
            currentAttackType = stats.attackType;
            currentMoveType = stats.moveType;
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            handleAnimations();
            if (currentHealth <= 0)
            {
                return;
            }
            handleMovment();
            handleShooting();
        }

        public virtual void init()
        {
            
        }

        public virtual void tick() {

        }

        public virtual void handleMovment()
        {
            //Raw Movement
            movement = controller.movement;
            yVelocity += gravity * Time.deltaTime;
            if (groundCheck.isGrounded)
            {
                yVelocity = 0f;
                if (movement.y > 0)
                {
                    yVelocity = stats.jumpForce;
                }
            }
            movement.y = yVelocity;

            //Calculated movement(Final)
            rb.velocity = movement * currentSpeed;
        }

        public virtual void handleShooting()
        {
            isShooting = controller.shoot;
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.z = 0;
            direction = direction.normalized;
            direction *= 0.8f; //Spacing from sprite
            if (isShooting && shootTimer >= stats.fireRate)
            {
                shootTimer = 0;
                Projectile bullet = Instantiate(stats.bullet, transform.position + direction, Quaternion.identity).GetComponent<Projectile>(); //TODO: Do controller aim
                bullet.gameObject.layer = LayerMask.NameToLayer("PlayerBullet"); //<-- name's different for everyone
                bullet.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
                bullet.init(direction, name);
            }
            shootTimer += Time.deltaTime;
        }

        protected virtual void handleAnimations()
        {
            animator.SetFloat("MovementX", Mathf.Abs(movement.x));
            animator.SetBool("IsGrounded", groundCheck.isGrounded);
            animator.SetBool("IsShooting", isShooting);
            animator.SetBool("IsAlive", currentHealth > 0);
            if (movement.x != 0)
            {
                direction = Mathf.Sign(movement.x);
            }
            animator.SetFloat("Direction", direction);
        }
    }
}