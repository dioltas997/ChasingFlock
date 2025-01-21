using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Allign")]
    public class Allign : TargetSteeringBehaviour
    {
        public float maxAngularAcceleration = 10f;
        public float targetRadius = 5f;
        public float slowRadius = 10f;
        public float timeToTarget = 0.5f;
        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();
            
            float rotation = Mathf.DeltaAngle(character.orientation, target.orientation);
            float rotationSize = Mathf.Abs(rotation);

            if (rotationSize < targetRadius)
            {
                result.rotation = -character.rotation * 0.5f;
                result.velocity = Vector2.zero;

                if (Mathf.Abs(character.rotation) < 0.01f)
                {
                    result.rotation = 0;
                }
                
                return result;
            }

            float targetRotation;

            if (rotationSize > slowRadius)
            {
                targetRotation = character.MaxRotation;
            }
            else
            {
                targetRotation = character.MaxRotation * rotationSize / slowRadius;
            }

            targetRotation *= rotation / rotationSize;

            result.rotation = targetRotation - character.rotation;
            result.rotation /= timeToTarget;
            
            float angularAcceleration = Mathf.Abs(result.rotation);
            if (angularAcceleration > maxAngularAcceleration)
            {
                result.rotation /= angularAcceleration;
                result.rotation *= maxAngularAcceleration;
            }

            result.velocity = Vector2.zero;
            return result;
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();
            
            Vector3 characterDirection = new Vector3(Mathf.Cos(character.orientation * Mathf.Deg2Rad), Mathf.Sin(character.orientation * Mathf.Deg2Rad), 0);
            Vector3 targetDirection = new Vector3(Mathf.Cos(target.orientation * Mathf.Deg2Rad), Mathf.Sin(target.orientation * Mathf.Deg2Rad), 0);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(character.position, (Vector3)character.position + characterDirection * 2f);
            Gizmos.DrawLine(target.position, (Vector3)target.position + targetDirection * 2f);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(character.position, (Vector3)character.position + targetDirection * 2f);

            Gizmos.color = Color.yellow;
            GizmosDrawArc(character.position, target.orientation, targetRadius);

            Gizmos.color = Color.magenta;
            GizmosDrawArc(character.position, target.orientation, slowRadius);
        }
    }
}