using BY;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BY
{
    public static class AttackPatterns
    {
        public static void KeepShooting(EnemyController ec)
        {
            if (ec.shootTimer >= ec.stats.fireRate)
            {
                ec.shootTimer = 0;
                Projectile bullet = GameObject.Instantiate(ec.stats.bullet, ec.transform.position + (ec.transform.right * ec.direction * ec.stats.fireOffset), Quaternion.identity).GetComponent<Projectile>();
                bullet.init(ec.transform.right * ec.direction, ec.name);
                //bullet.transform.parent = GameObject.Find("Projectiles").transform;
            }
            ec.shootTimer += Time.deltaTime;
        }

        public static void TargetEntity(EnemyController ec)
        {
            if (ec.shootTimer >= ec.stats.fireRate)
            {
                ec.shootTimer = 0;
                Vector3 direction = ec.target.transform.position - ec.transform.position;
                direction.z = 0;
                direction = direction.normalized;
                Projectile bullet = GameObject.Instantiate(ec.stats.bullet, ec.transform.position + (ec.stats.fireOffset * direction), Quaternion.identity).GetComponent<Projectile>();
                bullet.init(direction, ec.name);
            }
            ec.shootTimer += Time.deltaTime;
        }
    }

}