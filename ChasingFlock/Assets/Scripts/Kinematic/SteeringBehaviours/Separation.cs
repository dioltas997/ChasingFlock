using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Separation")]
    public class Separation : MultiTargetSteeringBehaviour
    {
        public float treshold = 5f;
        
        public float delayCoefficient = 1f;
        
        public float maxAcceleration = 10f;
        
        
        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();
            
            foreach (Kinematic target in targets)
            {
                Vector2 direction = character.position - target.position;
                float distance = direction.magnitude;

                if (distance > 0 && distance < treshold)
                {
                    float strength = Mathf.Min(delayCoefficient /(distance * distance), maxAcceleration);
                    
                    direction.Normalize();
                    result.velocity += direction * strength;
                }
            }

            if (result.velocity.magnitude > maxAcceleration)
            {
                result.velocity = result.velocity.normalized * maxAcceleration;
            }
            
            result.rotation = 0;
            
            return result;
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(character.position, treshold);
        }
    }
}