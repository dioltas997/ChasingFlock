using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Seek")]
    public class Seek : TargetSteeringBehaviour
    {
        public float maxAcceleration = 10f;
        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();

            result.velocity = target.position - character.position;
            result.velocity.Normalize();
            result.velocity *= maxAcceleration;

            result.rotation = 0;
            
            return result;
        }
    }
}