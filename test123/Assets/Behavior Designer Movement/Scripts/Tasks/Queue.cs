using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Queue in a line using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("c671469908c78284c909ff1905020250", "454cd90f13f2a9a4f93ade9379f9b3c9")]
    public class Queue : NavMeshGroupMovement
    {
        [Tooltip("Agents less than this distance apart are neighbors")]
        [UnityEngine.Serialization.FormerlySerializedAs("neighborDistance")]
        public SharedFloat m_NeighborDistance = 10;
        [Tooltip("The distance that the agents should be separated")]
        [UnityEngine.Serialization.FormerlySerializedAs("separationDistance")]
        public SharedFloat m_SeparationDistance = 2;
        [Tooltip("The distance the the agent should look ahead to see if another agent is in the way")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxQueueAheadDistance")]
        public SharedFloat m_MaxQueueAheadDistance = 2;
        [Tooltip("The radius that the agent should check to see if another agent is in the way")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxQueueRadius")]
        public SharedFloat m_MaxQueueRadius = 20;
        [Tooltip("The multiplier to slow down if an agent is in front of the current agent")]
        [UnityEngine.Serialization.FormerlySerializedAs("slowDownSpeed")]
        public SharedFloat m_SlowDownSpeed = 0.15f;
        [Tooltip("The target to seek towards")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;

        // The agents will always be flocking so always return running
        public override TaskStatus OnUpdate()
        {
            // Determine a destination for each agent
            for (int i = 0; i < m_Agents.Length; ++i) {
                if (AgentAhead(i)) {
                    SetDestination(i, m_Transforms[i].position + m_Transforms[i].forward * m_SlowDownSpeed.Value + DetermineSeparation(i));
                } else {
                    SetDestination(i, m_Target.Value.transform.position);
                }
            }
            return TaskStatus.Running;
        }

        // Returns the agent that is ahead of the current agent
        private bool AgentAhead(int index)
        {
            // queueAhead is the distance in front of the current agent
            var queueAhead = Velocity(index) * m_MaxQueueAheadDistance.Value;
            for (int i = 0; i < m_Agents.Length; ++i) {
                // Return the first agent that is ahead of the current agent
                if (index != i && Vector3.SqrMagnitude(queueAhead - m_Transforms[i].position) < m_MaxQueueRadius.Value) {
                    return true;
                }
            }
            return false;
        }

        // Determine the separation between the current agent and all of the other agents also queuing
        private Vector3 DetermineSeparation(int agentIndex)
        {
            var separation = Vector3.zero;
            int neighborCount = 0;
            var agentTransform = m_Transforms[agentIndex];
            // Loop through each agent to determine the separation
            for (int i = 0; i < m_Agents.Length; ++i) {
                // The agent can't compare against itself
                if (agentIndex != i) {
                    // Only determine the parameters if the other agent is its neighbor
                    if (Vector3.SqrMagnitude(m_Transforms[i].position - agentTransform.position) < m_NeighborDistance.Value) {
                        // This agent is the neighbor of the original agent so add the separation
                        separation += m_Transforms[i].position - agentTransform.position;
                        neighborCount++;
                    }
                }
            }

            // Don't move if there are no neighbors
            if (neighborCount == 0) {
                return Vector3.zero;
            }
            // Normalize the value
            return ((separation / neighborCount) * -1).normalized * m_SeparationDistance.Value;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_NeighborDistance = 10;
            m_SeparationDistance = 2;
            m_MaxQueueAheadDistance = 2;
            m_MaxQueueRadius = 20;
            m_SlowDownSpeed = 0.15f;
        }
    }
}