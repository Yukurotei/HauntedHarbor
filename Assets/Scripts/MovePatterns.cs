using BY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BY
{
    public static class MovePatterns
    {
        public static void MoveOnPlatform(EnemyController ec)
        {
            float xDist = 0.15f * ec.spriteSize.x;
            Vector3 leftOffset = Vector2.left * xDist;
            Vector3 rightOffset = Vector2.right * xDist;
            float yDist = 0.5f * ec.spriteSize.y;
            Vector3 downLeftOffset = (Vector3.down * yDist) + leftOffset;
            Vector3 downRightOffset = (Vector3.down * yDist) + rightOffset;
            RaycastHit2D hitLeft = Physics2D.Raycast(ec.transform.position + leftOffset, Vector2.left, xDist, ec.LayerMask);
            RaycastHit2D hitRight = Physics2D.Raycast(ec.transform.position + rightOffset, Vector2.right, xDist, ec.LayerMask);
            RaycastHit2D hitDownLeft = Physics2D.Raycast(ec.transform.position + downLeftOffset, Vector2.down, 1, ec.LayerMask);
            RaycastHit2D hitDownRight = Physics2D.Raycast(ec.transform.position + downRightOffset, Vector2.down, 1, ec.LayerMask);
            if (ec.movement.x == 0)
            {
                ec.movement.x = Mathf.Sign(Random.Range(-1, 1));
            }
            if (ec.movement.x < 0 && (hitLeft.collider != null || hitDownLeft.collider == null))
            {
                ec.movement.x *= -1;
            } else if (ec.movement.x > 0 && (hitRight.collider != null || hitDownRight.collider == null))
            {
                ec.movement.x *= -1;
            }
        }

        public static void CircleAroundPoint(EnemyController ec)
        {
            if (ec.movement.x == 0)
            {
                ec.movement.x = Mathf.Sign(Random.Range(-1, 1));
            }
            float radians = ec.angle * Mathf.Deg2Rad;
            float xPos = ec.spawnLocation.x + (ec.radius * Mathf.Cos(radians));
            float yPos = ec.spawnLocation.y + (ec.radius * Mathf.Sin(radians));
            ec.transform.position = new Vector3(xPos, yPos, 0);
            ec.angle += ec.movement.x * ec.currentSpeed * Time.deltaTime;
            ec.angle %= 360;
        }

        public static void Jumping(EnemyController ec)
        {
            if (ec.movement.x == 0)
            {
                ec.movement.x = Mathf.Sign(Random.Range(-1, 1));
            }
            ec.radius += ec.movement.x * ec.currentSpeed * Time.deltaTime; //If
            ec.transform.position = new Vector3(ec.spawnLocation.x, ec.spawnLocation.y + ec.radius, 0);
            if (Mathf.Abs(ec.radius) > ec.range)
            {
                ec.radius = ec.range * Mathf.Sign(ec.radius);
                ec.movement.x *= -1;
            }
        }
    }

}