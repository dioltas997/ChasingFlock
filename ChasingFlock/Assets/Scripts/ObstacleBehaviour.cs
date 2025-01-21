using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        [Header("Movement Settings")]
        public Vector2 movementDirection = Vector2.right;
        [Range(5.0f, 20.0f)]public float speed = 5f;

        [Header("Collision Detection")] 
        public LayerMask collisionLayer;
        public LayerMask agentLayer;
        public Color originalColor;
        public Color agentCollisionColor;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            rb.freezeRotation = true;
            
            spriteRenderer.color = originalColor;
            
            movementDirection.Normalize();
        }

        private void FixedUpdate()
        {
            MoveObstacle();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + movementDirection);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if ((collisionLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Debug.Log($"collision with {other.gameObject.name}. Changing direction");
                movementDirection = -movementDirection;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((agentLayer.value & (1 << other.gameObject.layer)) > 0)
            {
                Debug.Log($"Collision with {other.gameObject.name}. Changing color to {agentCollisionColor}");
                spriteRenderer.color = agentCollisionColor;
            }
        }

        private void MoveObstacle()
        {
            rb.velocity = movementDirection * speed;
        }
    }
}
