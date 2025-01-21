using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Movement
{
    public class KinematicBehaviour : MonoBehaviour
    {
        
        #region Properties
        public Kinematic Kinematic { get; private set; }
        #endregion

        [Header("Kinematic Constraints")]
        [Tooltip("The maximum linear speed that the object can reach")]
        [SerializeField] private float maxSpeed = 10;

        [Tooltip("The maximum angular speed that the object can reach")] [SerializeField]
        private float maxRotation = 10;
        
        [Header("Behaviour properties")]
        public bool hasBehaviour = false;
        [SerializeField] private SteeringBehaviour behaviour;
        [Header("Target properties")]
        public bool hasTarget = false;
        public GameObject targetGameObject;
        [Header("MultiTarget properties")]
        public bool hasMultiTarget = false;
        public GameObject[] targetGameObjects;
        public float lookForTargetRadius = 5f;
        public LayerMask targetLayerMask;

        #region Unity Events
        private void Awake()
        {
            Kinematic = new Kinematic(maxSpeed, maxRotation);

        }


        private void Start()
        {
            if (hasBehaviour)
            {
                AssignSteeringBehaviourProperties();
            }
            
            Kinematic.position = transform.position;
            Kinematic.orientation = transform.eulerAngles.z;
            
            Kinematic.velocity = Vector2.zero;
            Kinematic.rotation = 0;
        }

        private void Update()
        {
            Move();
            
            if (hasBehaviour)
            {
               UpdateKinematic();
            }
        }
        private void OnDrawGizmos()
        {
            if (Kinematic != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)Kinematic.velocity);
            }

            if (hasBehaviour)
            {
                behaviour.DrawGizmos();
            }
        }
        #endregion



        private void Move()
        {
            transform.position += (Vector3)Kinematic.velocity * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, 0f, Kinematic.rotation * Time.deltaTime);
            
            Kinematic.position = transform.position;
            Kinematic.orientation = transform.eulerAngles.z;

            if (Kinematic.velocity.magnitude > maxSpeed)
            {
                Kinematic.velocity.Normalize();
                Kinematic.velocity *= maxSpeed;
            }

            if (Mathf.Abs(Kinematic.rotation) > maxRotation)
            {
                Kinematic.rotation = maxRotation * Kinematic.rotation/Mathf.Abs(Kinematic.rotation);
            }
        }
        private void UpdateKinematic()
        {
            SteeringOutput output = behaviour.GetSteering();
            if(output == null) return;
            
            
            Kinematic.velocity += output.velocity * Time.deltaTime;
            Kinematic.rotation += output.rotation * Time.deltaTime;

        }

        private void AssignSteeringBehaviourProperties()
        {
            if (behaviour == null)
            {
                Debug.LogWarning($"the game object {name} is missing a {nameof(SteeringBehaviour)}");
            }
            else
            {
                behaviour.Character = Kinematic;

                if (behaviour.Character == null)
                {
                    Debug.LogError($"the game object {name} is missing a {nameof(SteeringBehaviour)}");
                }

                if (hasTarget)
                {
                    TargetSteeringBehaviour targetSteeringBehaviour = behaviour as TargetSteeringBehaviour;
                    if (targetSteeringBehaviour != null)
                    {
                        Debug.Log($"the game object {name} has a behaviour of type {nameof(TargetSteeringBehaviour)}");

                        if (targetGameObject == null)
                        {
                            Debug.LogError($"the target game object is missing from {name}");
                        }

                        KinematicBehaviour targetKinematicBehaviour = targetGameObject.GetComponent<KinematicBehaviour>();
                        if (targetKinematicBehaviour == null)
                        {
                            Debug.LogError($"the game object {targetGameObject} is missing a {nameof(targetKinematicBehaviour)} component");
                        }
                        else
                        {
                            targetSteeringBehaviour.SetTarget(targetKinematicBehaviour.Kinematic);
                        }
                    }
                }

                if (hasMultiTarget)
                {
                    MultiTargetSteeringBehaviour targetSteeringBehaviour = behaviour as MultiTargetSteeringBehaviour;
                    
                    if (targetSteeringBehaviour != null)
                    {
                        Debug.Log($"the game object {name} has a behaviour of type {nameof(MultiTargetSteeringBehaviour)}");
                        targetSteeringBehaviour.InitializeTargets();

                        if (targetGameObjects == null || targetGameObjects.Length == 0)
                        {
                            Debug.LogError($"the target game objects are missing from {name}");
                        }
                        else
                        {
                            foreach (GameObject targetGO in targetGameObjects)
                            {
                                if (targetGO == null) continue;
                                
                                KinematicBehaviour targetKinematicBehaviour = targetGO.GetComponent<KinematicBehaviour>();

                                if (targetKinematicBehaviour == null)
                                {
                                    Debug.LogError($"the game object {targetGO} is missing a {nameof(MultiTargetSteeringBehaviour)} component");
                                }
                                else
                                {
                                    Kinematic targetKinematic = targetKinematicBehaviour.Kinematic;
                                    targetSteeringBehaviour.AddTarget(targetKinematic);
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<Kinematic> LookForTargets()
        {
            List<Kinematic> targets = new List<Kinematic>();
            Collider2D[] targetsColliders = Physics2D.OverlapCircleAll(transform.position, lookForTargetRadius, targetLayerMask);
            foreach (Collider2D targetCollider in targetsColliders)
            {
                KinematicBehaviour targetKinematicBehaviour = targetCollider.GetComponent<KinematicBehaviour>();
                if (targetKinematicBehaviour == null)
                {
                    Debug.LogError($"the game object {targetCollider} is missing a {nameof(KinematicBehaviour)}");
                    continue;
                }
                else
                {
                    targets.Add(targetKinematicBehaviour.Kinematic);
                }
            }
            return targets;
        }
    }
}
