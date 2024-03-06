using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Flee from the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("e5f0ffa5bd82433428ba4d2dd58d57d8", "e1a2340aca5184f4ba0f3e3163864b8e")]
    public class Flee : NavMeshMovement
    {
        [Tooltip("The agent has fleed when the magnitude is greater than this value")]
        [UnityEngine.Serialization.FormerlySerializedAs("fleedDistance")]
        public SharedFloat m_FleedDistance = 20;
        [Tooltip("The distance to look ahead when fleeing")]
        [UnityEngine.Serialization.FormerlySerializedAs("lookAheadDistance")]
        public SharedFloat m_LookAheadDistance = 5;
        [Tooltip("The GameObject that the agent is fleeing from")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;

        private bool m_HasMoved;

        public override void OnStart()
        {
            base.OnStart();

            m_HasMoved = false;

            SetDestination(Target());
        }

        // Flee from the target. Return success once the agent has fleed the target by moving far enough away from it
        // Return running if the agent is still fleeing
        public override TaskStatus OnUpdate()
        {
            if (Vector3.Magnitude(transform.position - m_Target.Value.transform.position) > m_FleedDistance.Value) {
                return TaskStatus.Success;
            }

            if (HasArrived()) {
                if (!m_HasMoved) {
                    return TaskStatus.Failure;
                }
                if (!SetDestination(Target())) {
                    return TaskStatus.Failure;
                }
                m_HasMoved = false;
            } else {
                // If the agent is stuck the task shouldn't continue to return a status of running.
                var velocityMagnitude = Velocity().sqrMagnitude;
                if (m_HasMoved && velocityMagnitude <= 0f) {
                    return TaskStatus.Failure;
                }
                m_HasMoved = velocityMagnitude > 0f;
            }

            return TaskStatus.Running;
        }

        // Flee in the opposite direction
        private Vector3 Target()
        {
            return transform.position + (transform.position - m_Target.Value.transform.position).normalized * m_LookAheadDistance.Value;
        }

        // Return false if the position isn't valid on the NavMesh.
        protected override bool SetDestination(Vector3 destination)
        {
            if (!SamplePosition(ref destination)) {
                return false;
            }
            return base.SetDestination(destination);
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_FleedDistance = 20;
            m_LookAheadDistance = 5;
            m_Target = null;
        }
    }
}