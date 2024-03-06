using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Follows the specified target using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("815ba0528c01fe940bd4d5b51bf80773", "b17f9c5419e855948badb45ca05a4fcd")]
    public class Follow : NavMeshMovement
    {
        [Tooltip("The GameObject that the agent is following")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;
        [Tooltip("Start moving towards the target if the target is further than the specified distance")]
        [UnityEngine.Serialization.FormerlySerializedAs("moveDistance")]
        public SharedFloat m_MoveDistance = 2;

        private Vector3 lastTargetPosition;
        private bool hasMoved;

        public override void OnStart()
        {
            base.OnStart();

            if (m_Target.Value == null) {
                return;
            }

            lastTargetPosition = m_Target.Value.transform.position + Vector3.one * (m_MoveDistance.Value + 1);
            hasMoved = false;
        }

        // Follow the target. The task will never return success as the agent should continue to follow the target even after arriving at the destination.
        public override TaskStatus OnUpdate()
        {
            if (m_Target.Value == null) {
                return TaskStatus.Failure;
            }

            // Move if the target has moved more than the moveDistance since the last time the agent moved.
            var targetPosition = m_Target.Value.transform.position;
            if ((targetPosition - lastTargetPosition).magnitude >= m_MoveDistance.Value) {
                SetDestination(targetPosition);
                lastTargetPosition = targetPosition;
                hasMoved = true;
            } else {
                // Stop moving if the agent is within the moveDistance of the target.
                if (hasMoved && (targetPosition - transform.position).magnitude < m_MoveDistance.Value) {
                    Stop();
                    hasMoved = false;
                    lastTargetPosition = targetPosition;
                }
            }

            return TaskStatus.Running;
        }

        public override void OnReset()
        {
            base.OnReset();
            m_Target = null;
            m_MoveDistance = 2;
        }
    }
}