using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Search for a target by combining the wander, within hearing range, and the within seeing range tasks using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("0ebd85be99a56804b9b63041ad4a7d42", "6e61dc457d90a294783c6cae5443b5f6")]
    public class Search : NavMeshMovement
    {
        [Tooltip("Should the 2D version be used?")]
        [UnityEngine.Serialization.FormerlySerializedAs("usePhysics2D")]
        public bool m_UsePhysics2D;
        [Tooltip("Minimum distance ahead of the current position to look ahead for a destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("minWanderDistance")]
        public SharedFloat m_MinWanderDistance = 20;
        [Tooltip("Maximum distance ahead of the current position to look ahead for a destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxWanderDistance")]
        public SharedFloat m_MaxWanderDistance = 20;
        [Tooltip("The amount that the agent rotates direction")]
        [UnityEngine.Serialization.FormerlySerializedAs("wanderRate")]
        public SharedFloat m_WanderRate = 1;
        [Tooltip("The minimum length of time that the agent should pause at each destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("minPauseDuration")]
        public SharedFloat m_MinPauseDuration = 0;
        [Tooltip("The maximum length of time that the agent should pause at each destination (zero to disable)")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxPauseDuration")]
        public SharedFloat m_MaxPauseDuration = 0;
        [Tooltip("The maximum number of retries per tick (set higher if using a slow tick time)")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetRetries")]
        public SharedInt m_TargetRetries = 1;
        [Tooltip("The field of view angle of the agent (in degrees)")]
        [UnityEngine.Serialization.FormerlySerializedAs("fieldOfViewAngle")]
        public SharedFloat m_FieldOfViewAngle = 90;
        [Tooltip("The distance that the agent can see")]
        [UnityEngine.Serialization.FormerlySerializedAs("viewDistance")]
        public SharedFloat m_ViewDistance = 30;
        [Tooltip("The LayerMask of the objects to ignore when performing the line of sight check")]
        [UnityEngine.Serialization.FormerlySerializedAs("ignoreLayerMask")]
        public LayerMask m_IgnoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        [Tooltip("The offset to apply to 2D angles")]
        [UnityEngine.Serialization.FormerlySerializedAs("angleOffset2D")]
        public SharedFloat m_AngleOffset2D;
        [Tooltip("Should the search end if audio was heard?")]
        [UnityEngine.Serialization.FormerlySerializedAs("senseAudio")]
        public SharedBool m_SenseAudio = true;
        [Tooltip("How far away the unit can hear")]
        [UnityEngine.Serialization.FormerlySerializedAs("hearingRadius")]
        public SharedFloat m_HearingRadius = 30;
        [Tooltip("The raycast offset relative to the pivot position")]
        [UnityEngine.Serialization.FormerlySerializedAs("offset")]
        public SharedVector3 m_Offset;
        [Tooltip("The target raycast offset relative to the pivot position")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetOffset")]
        public SharedVector3 m_TargetOffset;
        [Tooltip("The LayerMask of the objects that we are searching for")]
        [UnityEngine.Serialization.FormerlySerializedAs("objectLayerMask")]
        public LayerMask m_TargetLayerMask;
        [Tooltip("Specifies the maximum number of colliders that the physics cast can collide with")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxCollisionCount")]
        public int m_MaxCollisionCount = 200;
        [Tooltip("Should the target bone be used?")]
        [UnityEngine.Serialization.FormerlySerializedAs("useTargetBone")]
        public SharedBool m_UseTargetBone;
        [Tooltip("The target's bone if the target is a humanoid")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetBone")]
        public SharedHumanBodyBones m_TargetBone;
        [Tooltip("Should a debug look ray be drawn to the scene view?")]
        [UnityEngine.Serialization.FormerlySerializedAs("drawDebugRay")]
        public SharedBool m_DrawDebugRay;
        [Tooltip("The further away a sound source is the less likely the agent will be able to hear it. " +
                 "Set a threshold for the the minimum audibility level that the agent can hear")]
        [UnityEngine.Serialization.FormerlySerializedAs("audibilityThreshold")]
        public SharedFloat m_AudibilityThreshold = 0.05f;
        [Tooltip("The object that is found")]
        [UnityEngine.Serialization.FormerlySerializedAs("returnedObject")]
        public SharedGameObject m_ReturnedObject;

        private float m_PauseTime;
        private float m_DestinationReachTime;

        private Collider[] m_OverlapColliders;
        private Collider2D[] m_Overlap2DColliders;

        // Keep searching until an object is seen or heard (if senseAudio is enabled)
        public override TaskStatus OnUpdate()
        {
            if (HasArrived()) {
                // The agent should pause at the destination only if the max pause duration is greater than 0
                if (m_MaxPauseDuration.Value > 0) {
                    if (m_DestinationReachTime == -1) {
                        m_DestinationReachTime = Time.time;
                        m_PauseTime = Random.Range(m_MinPauseDuration.Value, m_MaxPauseDuration.Value);
                    } else if (m_DestinationReachTime + m_PauseTime <= Time.time) {
                        // Only reset the time if a destination has been set.
                        if (TrySetTarget()) {
                            m_DestinationReachTime = -1;
                        }
                    }
                } else {
                    TrySetTarget();
                }
            }

            // Detect if any objects are within sight
            if (m_UsePhysics2D) {
                if (m_Overlap2DColliders == null) {
                    m_Overlap2DColliders = new Collider2D[m_MaxCollisionCount];
                }
                m_ReturnedObject.Value = MovementUtility.WithinSight2D(transform, m_Offset.Value, m_FieldOfViewAngle.Value, m_ViewDistance.Value, m_Overlap2DColliders, m_TargetLayerMask, m_TargetOffset.Value, m_AngleOffset2D.Value, m_IgnoreLayerMask, m_DrawDebugRay.Value);
            } else {
                if (m_OverlapColliders == null) {
                    m_OverlapColliders = new Collider[m_MaxCollisionCount];
                }
                m_ReturnedObject.Value = MovementUtility.WithinSight(transform, m_Offset.Value, m_FieldOfViewAngle.Value, m_ViewDistance.Value, m_OverlapColliders, m_TargetLayerMask, m_TargetOffset.Value, m_IgnoreLayerMask, m_UseTargetBone.Value, m_TargetBone.Value, m_DrawDebugRay.Value);
            }
            // If an object was seen then return success
            if (m_ReturnedObject.Value != null) {
                return TaskStatus.Success;
            }

            // Detect if any object are within audio range (if enabled)
            if (m_SenseAudio.Value) {
                if (m_UsePhysics2D) {
                    m_ReturnedObject.Value = MovementUtility.WithinHearingRange2D(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_HearingRadius.Value, m_Overlap2DColliders, m_TargetLayerMask);
                } else {
                    m_ReturnedObject.Value = MovementUtility.WithinHearingRange(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_HearingRadius.Value, m_OverlapColliders, m_TargetLayerMask);
                }
                // If an object was heard then return success
                if (m_ReturnedObject.Value != null) {
                    return TaskStatus.Success;
                }
            }

            // No object has been seen or heard so keep searching
            return TaskStatus.Running;
        }

        private bool TrySetTarget()
        {
            var direction = transform.forward;
            var attempts = m_TargetRetries.Value;
            var destination = transform.position;
            while (attempts > 0) {
                direction = direction + Random.insideUnitSphere * m_WanderRate.Value;
                destination = transform.position + direction.normalized * Random.Range(m_MinWanderDistance.Value, m_MaxWanderDistance.Value);
                if (SamplePosition(ref destination)) {
                    SetDestination(destination);
                    return true;
                }
                attempts--;
            }
            return false;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_MinWanderDistance = 20;
            m_MaxWanderDistance = 20;
            m_WanderRate = 2;
            m_MinPauseDuration = 0;
            m_MaxPauseDuration = 0;
            m_TargetRetries = 1;
            m_FieldOfViewAngle = 90;
            m_ViewDistance = 30;
            m_AngleOffset2D = 0;
            m_DrawDebugRay = false;
            m_SenseAudio = true;
            m_HearingRadius = 30;
            m_AudibilityThreshold = 0.05f;
        }
    }
}