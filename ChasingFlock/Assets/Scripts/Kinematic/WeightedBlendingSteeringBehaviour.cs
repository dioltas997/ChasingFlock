using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    [Serializable]
    public class BehaviourAndWeight
    {
        public SteeringBehaviour behaviour;
        public float weight;
    }
    
    [CreateAssetMenu(menuName = "AI/Movement/Steering Behaviours/Weighted Blending")]
    public class WeightedBlendingSteeringBehaviour : TargetSteeringBehaviour
    {
        [SerializeField] private List<BehaviourAndWeight> behaviours;
        
        public override SteeringOutput GetSteering()
        {
            SteeringOutput result = new SteeringOutput();

            foreach (BehaviourAndWeight b in behaviours)
            {
                SteeringOutput res = b.behaviour.GetSteering();
                result.velocity += res.velocity * b.weight;
                result.rotation += res.rotation * b.weight;
            }

            if (result.velocity.magnitude > character.MaxSpeed)
            {
                result.velocity = result.velocity.normalized * character.MaxSpeed;
            }

            result.rotation = Mathf.Max(result.rotation, character.MaxRotation);

            return result;
        }
    }
}