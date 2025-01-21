using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    public abstract class MultiTargetSteeringBehaviour : SteeringBehaviour
    {
        protected List<Kinematic> targets;

        public void InitializeTargets()
        {
            targets = new List<Kinematic>();
        }

        public void AddTarget(Kinematic target)
        {
            if(target != null && !targets.Contains(target))
                targets.Add(target);
            else
            {
                Debug.LogWarning("Cannot add Target to MultiTargetSteeringBehaviour");
            }
        }
    }
}