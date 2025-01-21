using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.AI.Movement
{
    /**
     *  this class model kinematic data of an object
     */
    [System.Serializable]
    public class Kinematic
    {
        #region Member Variables
        [Header("Static Properties")]
        [Tooltip("The current position of the object")] public Vector2 position;
        [Tooltip("The current orientation (z- rotation abgle) of the object, in degrees")] public float orientation;
        
        [Header("Kinematic Properties")]
        [Tooltip("The current velocity of the object")] public Vector2 velocity;
        [Tooltip("The current angular velocity of the object")] public float rotation;
        [Header("Kinematic Constraints")]
        [Tooltip("The maximum linear speed that the object can reach")] private readonly float maxSpeed;
        [Tooltip("The maximum angular speed that the object can reach")] private readonly float maxRotation;
        #endregion
        #region Properties
        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
            private set
            {
                Debug.LogError("Trying to modify MaxSpeed");
            }
        }

        public float MaxRotation
        {
            get
            {
                return maxRotation;
            }

            private set
            {
                Debug.LogError("Trying to modify MaxRotation");
            }
        }
        
        #endregion

        #region Constructors
        public Kinematic(float maxSpeed, float maxRotation)
        {
            this.maxSpeed = maxSpeed;
            this.maxRotation = maxRotation;
        }
        #endregion
        #region Public Methods
        
        #endregion
    }
}