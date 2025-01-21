using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    public class SteeringOutput
    {
        public Vector2 velocity;
        public float rotation;

        public SteeringOutput()
        {
            velocity = Vector2.zero;
            rotation = 0;
        }
    }
}