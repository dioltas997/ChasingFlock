using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    public abstract class TargetSteeringBehaviour : SteeringBehaviour
    {
        protected Kinematic target;

        public void SetTarget(Kinematic target)
        {
            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }
            this.target = target;
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();
            if (target == null)
            {
                Debug.LogError("No target assigned!");
                return;
            }
        }
    }
}
