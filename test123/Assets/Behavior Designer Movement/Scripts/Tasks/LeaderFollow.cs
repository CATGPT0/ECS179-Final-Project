using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Follow the leader using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("9ef93ef9a60e48449a642b1b3b2b577d", "ceded4836fa9bc24f964ec6fadccdc40")]
    public class LeaderFollow : NavMeshGroupMovement
    {
        [Tooltip("Agents less than this distance apart are neighbors")]
        [UnityEngine.Serialization.FormerlySerializedAs("neighborDistance")]
        public SharedFloat m_NeighborDistance = 10;
        [Tooltip("How far behind the leader the agents should follow the leader")]
        [UnityEngine.Serialization.FormerlySerializedAs("leaderBehindDistance")]
        public SharedFloat m_LeaderBehindDistance = 2;
        [Tooltip("The distance that the agents should be separated")]
        [UnityEngine.Serialization.FormerlySerializedAs("separationDistance")]
        public SharedFloat m_SeparationDistance = 2;
        [Tooltip("The agent is getting too close to the front of the leader if they are within the aheadDistance")]
        [UnityEngine.Serialization.FormerlySerializedAs("aheadDistance")]
        public SharedFloat m_AheadDistance = 2;
        [Tooltip("The leader to follow")]
        [UnityEngine.Serialization.FormerlySerializedAs("leader")]
        public SharedGameObject m_Leader;

        // component cache
        protected Transform m_LeaderTransform;
        protected NavMeshAgent m_LeaderAgent;

        public override void OnStart()
        {
            m_LeaderTransform = m_Leader.Value.transform;
            m_LeaderAgent = m_Leader.Value.GetComponent<NavMeshAgent>();

            base.OnStart();
        }

        // The agents will always be following the leader so always return running
        public override TaskStatus OnUpdate()
        {
            var behindPosition = LeaderBehindPosition();
            // Determine a destination for each agent
            for (int i = 0; i < m_Agents.Length; ++i) {
                // Get out of the way of the leader if the leader is currently looking at the agent and is getting close
                if (LeaderLookingAtAgent(i) && Vector3.Magnitude(m_LeaderTransform.position - m_Transforms[i].position) < m_AheadDistance.Value) {
                    SetDestination(i, m_Transforms[i].position + (m_Transforms[i].position - m_LeaderTransform.position).normalized * m_AheadDistance.Value);
                } else {
                    // The destination is the behind position added to the separation vector
                    SetDestination(i, behindPosition + DetermineSeparation(i));
                }
            }
            return TaskStatus.Running;
        }

        private Vector3 LeaderBehindPosition()
        {
            // The behind position is the normalized inverse of the leader's velocity multiplied by the leaderBehindDistance
            return m_LeaderTransform.position + (-m_LeaderAgent.velocity).normalized * m_LeaderBehindDistance.Value;
        }

        // Determine the separation between the current agent and all of the other agents also following the leader
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

        // Use the dot product to determine if the leader is looking at the current agent
        public bool LeaderLookingAtAgent(int agentIndex)
        {
            return Vector3.Dot(m_LeaderTransform.forward, m_Transforms[agentIndex].forward) < -0.5f;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_NeighborDistance = 10;
            m_LeaderBehindDistance = 2;
            m_SeparationDistance = 2;
            m_AheadDistance = 2;
            m_Leader = null;
        }
    }
}