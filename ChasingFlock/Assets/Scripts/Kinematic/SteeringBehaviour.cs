using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    public abstract class SteeringBehaviour : ScriptableObject
    {
        public Kinematic Character
        {
            get
            {
                return character;
            }

            set
            {
                character = value;
            }
        }
        protected Kinematic character;

        public abstract SteeringOutput GetSteering();

        public virtual void DrawGizmos()
        {
            if (character == null) return;
        }

        protected void GizmosDrawArc(Vector2 center, float orientation, float angle)
        {
            float halfAngle = angle * 0.5f;

            Vector3 direction1 = new Vector3(Mathf.Cos((orientation - halfAngle) * Mathf.Deg2Rad), Mathf.Sin((orientation - halfAngle) * Mathf.Deg2Rad), 0);
            Vector3 direction2 = new Vector3(Mathf.Cos((orientation + halfAngle) * Mathf.Deg2Rad), Mathf.Sin((orientation + halfAngle) * Mathf.Deg2Rad), 0);

            Gizmos.DrawLine(center, (Vector3)center + direction1 * 2f);
            Gizmos.DrawLine(center, (Vector3)center + direction2 * 2f);
        }
    }
}