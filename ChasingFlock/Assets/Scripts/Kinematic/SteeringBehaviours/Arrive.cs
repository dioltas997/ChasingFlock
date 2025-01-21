using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Arrive")]
    public class Arrive : TargetSteeringBehaviour
    {
        public float maxAcceleration = 10f;

        public float targetRadius = 2f;
        public float slowRadius = 10f;

        public float timeToTarget = .3f;
        public override SteeringOutput GetSteering()
        {
            if (target == null)
            {
                Debug.LogError("No target selected");
                return null;
            }
            SteeringOutput result = new SteeringOutput();

            Vector2 direction = target.position - character.position;
            float distance = direction.magnitude;

            if (distance < targetRadius)
            {
                Vector2 currentVelocity = -character.velocity * 0.5f;
                result.rotation = 0;

                if (character.velocity.magnitude < 0.1f)
                {
                    result.velocity = Vector2.zero;
                }

                return result;
            }

            float targetSpeed;

            if (distance > slowRadius)
            {
                targetSpeed = character.MaxSpeed;
            }
            else
            {
                targetSpeed = character.MaxSpeed * distance / slowRadius;
            }
            
            Vector2 targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            result.velocity = targetVelocity - character.velocity;
            result.velocity /= timeToTarget;

            if (result.velocity.magnitude > maxAcceleration)
            {
                result.velocity.Normalize();
                result.velocity *= maxAcceleration;
            }

            result.rotation = 0;
            return result;
        }
        
        public override void DrawGizmos()
        {
            base.DrawGizmos();
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere((Vector3)target.position, slowRadius);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere((Vector3)target.position, targetRadius);
        }
    }
}