using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BY
{
    public class GroundCheck : MonoBehaviour
    {
        public bool isGrounded = false;

        public void OnTriggerStay2D(Collider2D other)
        {
            isGrounded = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            isGrounded = false;
        }
    }
}
