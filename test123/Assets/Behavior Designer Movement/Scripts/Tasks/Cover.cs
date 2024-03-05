using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Find a place to hide and move to it using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("c91b8fe3d68a9114dafd557a82d821d8", "67e27331b399ae14f9eb7a6debc1802d")]
    public class Cover : NavMeshMovement
    {
        [Tooltip("The distance to search for cover")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxCoverDistance")]
        public SharedFloat m_MaxCoverDistance = 1000;
        [Tooltip("The layermask of the available cover positions")]
        [UnityEngine.Serialization.FormerlySerializedAs("availableLayerCovers")]
        public LayerMask m_AvailableLayerCovers;
        [Tooltip("The maximum number of raycasts that should be fired before the agent gives up looking for an agent to find cover behind")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxRaycasts")]
        public SharedInt m_MaxRaycasts = 100;
        [Tooltip("How large the step should be between raycasts")]
        [UnityEngine.Serialization.FormerlySerializedAs("rayStep")]
        public SharedFloat m_RayStep = 1;
        [Tooltip("Once a cover point has been found, multiply this offset by the normal to prevent the agent from hugging the wall")]
        [UnityEngine.Serialization.FormerlySerializedAs("coverOffset")]
        public SharedFloat m_CoverOffset = 2;
        [Tooltip("Should the agent look at the cover point after it has arrived?")]
        [UnityEngine.Serialization.FormerlySerializedAs("lookAtCoverPoint")]
        public SharedBool m_LookAtCoverPoint = false;
        [Tooltip("The agent is done rotating to the cover point when the square magnitude is less than this value")]
        [UnityEngine.Serialization.FormerlySerializedAs("rotationEpsilon")]
        public SharedFloat m_RotationEpsilon = 0.5f;
        [Tooltip("Max rotation delta if lookAtCoverPoint")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxLookAtRotationDelta")]
        public SharedFloat m_MaxLookAtRotationDelta;

        private Vector3 m_CoverPoint;
        // The position to reach, offsetted from coverPoint
        private Vector3 m_CoverTarget;
        // Was cover found?
        private bool m_FoundCover;

        public override void OnStart()
        {
            RaycastHit hit;
            int raycastCount = 0;
            var direction = transform.forward;
            float step = 0;
            m_FoundCover = false;
            // Keep firing a ray until too many rays have been fired
            while (raycastCount < m_MaxRaycasts.Value) {
                var ray = new Ray(transform.position, direction);
                if (Physics.Raycast(ray, out hit, m_MaxCoverDistance.Value, m_AvailableLayerCovers.value)) {
                    // A suitable agent has been found. Find the opposite side of that agent by shooting a ray in the opposite direction from a point far away
                    if (hit.collider.Raycast(new Ray(hit.point - hit.normal * m_MaxCoverDistance.Value, hit.normal), out hit, Mathf.Infinity)) {
                        m_CoverPoint = hit.point;
                        m_CoverTarget = hit.point + hit.normal * m_CoverOffset.Value;
                        m_FoundCover = true;
                        break;
                    }
                }
                // Keep sweeiping along the y axis
                step += m_RayStep.Value;
                direction = Quaternion.Euler(0, transform.eulerAngles.y + step, 0) * Vector3.forward;
                raycastCount++;
            }

            if (m_FoundCover) {
                SetDestination(m_CoverTarget);
            }

            base.OnStart();
        }

        // Seek to the cover point. Return success as soon as the location is reached or the agent is looking at the cover point
        public override TaskStatus OnUpdate()
        {
            if (!m_FoundCover) {
                return TaskStatus.Failure;
            }
            if (HasArrived()) {
                var rotation = Quaternion.LookRotation(m_CoverPoint - transform.position);
                // Return success if the agent isn't going to look at the cover point or it has completely rotated to look at the cover point
                if (!m_LookAtCoverPoint.Value || Quaternion.Angle(transform.rotation, rotation) < m_RotationEpsilon.Value) {
                    return TaskStatus.Success;
                } else {
                    // Still needs to rotate towards the target
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_MaxLookAtRotationDelta.Value);
                }
            }

            return TaskStatus.Running;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnStart();

            m_MaxCoverDistance = 1000;
            m_MaxRaycasts = 100;
            m_RayStep = 1;
            m_CoverOffset = 2;
            m_LookAtCoverPoint = false;
            m_RotationEpsilon = 0.5f;
        }
    }
}