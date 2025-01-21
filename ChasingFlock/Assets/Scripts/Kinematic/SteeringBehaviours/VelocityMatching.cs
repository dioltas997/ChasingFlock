using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Velocity Matching")]
    public class VelocityMatching : TargetSteeringBehaviour
    {
        public float maxAcceleration = 10f;
        
        public float timeToTarget = 0.5f;

        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();

            result.velocity = target.velocity - character.velocity;
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
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(character.position, (Vector3)(character.position + target.velocity));
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(character.position, (Vector3)(character.position + character.velocity));
        }
    }
}