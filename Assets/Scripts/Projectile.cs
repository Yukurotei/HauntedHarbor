using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BY
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public float lifetime;
        public string parentName;
        public float damage;
        public bool isExploding;
        public int type;
        public AudioClip explode;
        Animator animator;
        Vector3 direction;
        Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            animator.SetFloat("Type", type);
        }

        // Update is called once per frame
        void Update()
        {
            if (isExploding)
            {
                rb.velocity = Vector2.zero;
                return;
            }
            rb.velocity = direction * speed;
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
            {
                animator.SetBool("IsExploding", true);
                isExploding = true;
            }
        }

        public void init(Vector3 dir, string parent)
        {
            parentName = parent;
            direction = dir;
            transform.SetParent(GameObject.Find("Level").transform.Find("Projectiles"));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*
            if (collision.transform.name == "Player" && parentName == "Player")
            {
                return;
            }
            */
            EnemyController s = collision.transform.GetComponentInParent<EnemyController>();
            if (s != null && !isExploding)
            {
                s.currentHealth -= damage;
            } else
            {
                Sprite ss = collision.transform.GetComponent<Sprite>();
                if (ss != null && !isExploding) 
                {
                    ss.currentHealth -= damage;
                }
            }
            animator.SetBool("IsExploding", true);
            isExploding = true;
        }

        public void Explode()
        {
            Destroy(gameObject);
        }
    }
}