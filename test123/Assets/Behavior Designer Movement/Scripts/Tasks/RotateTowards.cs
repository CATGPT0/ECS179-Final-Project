using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Rotates towards the specified rotation. The rotation can either be specified by a transform or rotation. If the transform "+
                     "is used then the rotation will not be used.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("04fb8138ea905c04ea39265f778fe1a4", "9bded0edc8b2a2f478fc28396fa41df2")]
    public class RotateTowards : Action
    {
        [Tooltip("Should the 2D version be used?")]
        [UnityEngine.Serialization.FormerlySerializedAs("usePhysics2D")]
        public bool m_UsePhysics2D;
        [Tooltip("The agent is done rotating when the angle is less than this value")]
        [UnityEngine.Serialization.FormerlySerializedAs("rotationEpsilon")]
        public SharedFloat m_RotationEpsilon = 0.5f;
        [Tooltip("The maximum number of angles the agent can rotate in a single tick")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxLookAtRotationDelta")]
        public SharedFloat m_MaxLookAtRotationDelta = 1;
        [Tooltip("Should the rotation only affect the Y axis?")]
        [UnityEngine.Serialization.FormerlySerializedAs("onlyY")]
        public SharedBool m_OnlyY;
        [Tooltip("The GameObject that the agent is rotating towards")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;
        [Tooltip("If target is null then use the target rotation")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetRotation")]
        public SharedVector3 m_TargetRotation;

        public override TaskStatus OnUpdate()
        {
            var rotation = Target();
            // Return a task status of success once we are done rotating
            if (Quaternion.Angle(transform.rotation, rotation) < m_RotationEpsilon.Value) {
                return TaskStatus.Success;
            }
            // We haven't reached the target yet so keep rotating towards it
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, m_MaxLookAtRotationDelta.Value);
            return TaskStatus.Running;
        }

        // Return targetPosition if targetTransform is null
        private Quaternion Target()
        {
            if (m_Target == null || m_Target.Value == null) {
                return Quaternion.Euler(m_TargetRotation.Value);
            }
            var position = m_Target.Value.transform.position - transform.position;
            if (m_OnlyY.Value) {
                position.y = 0;
            }
            if (m_UsePhysics2D) {
                var angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
                return Quaternion.AngleAxis(angle, Vector3.forward);
            }
            return Quaternion.LookRotation(position);
        }

        // Reset the public variables
        public override void OnReset()
        {
            m_UsePhysics2D = false;
            m_RotationEpsilon = 0.5f;
            m_MaxLookAtRotationDelta = 1f;
            m_OnlyY = false;
            m_Target = null;
            m_TargetRotation = Vector3.zero;
        }
    }
}