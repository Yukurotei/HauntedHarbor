using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BY
{
    public class Parrallax : MonoBehaviour
    {
        public Transform cameraTransform;
        Vector3 prevCameraPos;
        public bool isInfiniteScrollingX;
        public bool isInfiniteScrollingY;
        public Vector2 scrollingSpeed;
        Vector2 textureSize;
        // Start is called before the first frame update
        void Start()
        {
            cameraTransform = Camera.main.transform;
            prevCameraPos = cameraTransform.position;
            UnityEngine.Sprite sprite = GetComponent<UnityEngine.SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            textureSize.x = texture.width * transform.localScale.x / sprite.pixelsPerUnit;
            textureSize.y = texture.height * transform.localScale.y / sprite.pixelsPerUnit;
        }

        // LateUpdate is called once per frame
        void LateUpdate()
        {
            Vector3 deltaMovement = cameraTransform.position - prevCameraPos;
            transform.position += new Vector3(deltaMovement.x * scrollingSpeed.x, deltaMovement.y * scrollingSpeed.y, 0);
            prevCameraPos = cameraTransform.position;
            if (isInfiniteScrollingX)
            {
                if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureSize.x) //Check if we scrolled farther than the size of image
                {
                    float offsetX = (cameraTransform.position.x - transform.position.x) % textureSize.x;
                    transform.position = new Vector3(cameraTransform.position.x + offsetX, transform.position.y, 0);
                }
            }
            if (isInfiniteScrollingY)
            {
                if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureSize.y) //Check if we scrolled farther than the size of image
                {
                    float offsetY = (cameraTransform.position.y - transform.position.y) % textureSize.y;
                    transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetY, 0);
                }
            }
        }
    }
}
