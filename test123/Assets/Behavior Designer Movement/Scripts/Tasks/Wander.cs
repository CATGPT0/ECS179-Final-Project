using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Wander using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("c8e612848487a184f9090d416c932c47", "cc64e7434e679324c8cb39430f19eda8")]
    public class Wander : NavMeshMovement
    {
        [Tooltip("Minimum distance ahead of the current position to look ahead for a destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("minWanderDistance")]
        public SharedFloat m_MinWanderDistance = 20;
        [Tooltip("Maximum distance ahead of the current position to look ahead for a destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxWanderDistance")]
        public SharedFloat m_MaxWanderDistance = 20;
        [Tooltip("The maximum number of degrees that the agent can turn when wandering")]
        public SharedFloat m_MaxWanderDegrees = 5;
        [Tooltip("The minimum length of time that the agent should pause at each destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("minPauseDuration")]
        public SharedFloat m_MinPauseDuration = 0;
        [Tooltip("The maximum length of time that the agent should pause at each destination (zero to disable)")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxPauseDuration")]
        public SharedFloat m_MaxPauseDuration = 0;
        [Tooltip("The maximum number of retries per tick (set higher if using a slow tick time)")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetRetries")]
        public SharedInt m_TargetRetries = 1;

        private float m_PauseTime;
        private float m_DestinationReachTime;

        // There is no success or fail state with wander - the agent will just keep wandering
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
            return TaskStatus.Running;
        }

        private bool TrySetTarget()
        {
            var direction = transform.forward;
            var attempts = m_TargetRetries.Value;
            Vector3 destination;
            while (attempts > 0) {
                direction = Quaternion.Euler(0, Random.Range(-m_MaxWanderDegrees.Value, m_MaxWanderDegrees.Value), 0) * direction;
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
            m_MinWanderDistance = 20;
            m_MaxWanderDistance = 20;
            m_MaxWanderDegrees = 5;
            m_MinPauseDuration = 0;
            m_MaxPauseDuration = 0;
            m_TargetRetries = 1;
        }
    }
}