using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Flee")]
    public class Flee : TargetSteeringBehaviour
    {
        public float maxAcceleration = 10f;
        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();

            result.velocity = character.position - target.position;
            result.velocity.Normalize();
            result.velocity *= maxAcceleration;

            result.rotation = 0;
            
            return result;
        }
    }
}