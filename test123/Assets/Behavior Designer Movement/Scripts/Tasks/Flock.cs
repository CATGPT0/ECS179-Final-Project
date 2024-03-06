using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Flock around the scene using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("5c4c8ca7a2b26d648ad1b3009d8ee3d6", "29465febf85da33499a039c8ec393d64")]
    public class Flock : NavMeshGroupMovement
    {
        [Tooltip("Agents less than this distance apart are neighbors")]
        [UnityEngine.Serialization.FormerlySerializedAs("neighborDistance")]
        public SharedFloat m_NeighborDistance = 100;
        [Tooltip("How far the agent should look ahead when determine its pathfinding destination")]
        [UnityEngine.Serialization.FormerlySerializedAs("lookAheadDistance")]
        public SharedFloat m_LookAheadDistance = 5;
        [Tooltip("The greater the alignmentWeight is the more likely it is that the agents will be facing the same direction")]
        [UnityEngine.Serialization.FormerlySerializedAs("alignmentWeight")]
        public SharedFloat m_AlignmentWeight = 0.4f;
        [Tooltip("The greater the cohesionWeight is the more likely it is that the agents will be moving towards a common position")]
        [UnityEngine.Serialization.FormerlySerializedAs("cohesionWeight")]
        public SharedFloat m_CohesionWeight = 0.5f;
        [Tooltip("The greater the separationWeight is the more likely it is that the agents will be separated")]
        [UnityEngine.Serialization.FormerlySerializedAs("separationWeight")]
        public SharedFloat m_SeparationWeight = 0.6f;

        // The agents will always be flocking so always return running
        public override TaskStatus OnUpdate()
        {
            // Determine a destination for each agent
            for (int i = 0; i < m_Agents.Length; ++i) {
                Vector3 alignment, cohesion, separation;
                // determineFlockAttributes will determine which direction to head, which common position to move toward, and how far apart each agent is from one another,
                DetermineFlockParameters(i, out alignment, out cohesion, out separation);
                // Weigh each parameter to give one more of an influence than another
                var velocity = alignment * m_AlignmentWeight.Value + cohesion * m_CohesionWeight.Value + separation * m_SeparationWeight.Value;
                // Set the destination based on the velocity multiplied by the look ahead distance
                if (!SetDestination(i, m_Transforms[i].position + velocity * m_LookAheadDistance.Value)) {
                    // Go the opposite direction if the destination is invalid
                    velocity *= -1;
                    SetDestination(i, m_Transforms[i].position + velocity * m_LookAheadDistance.Value);
                }
            }
            return TaskStatus.Running;
        }

        // Determine the three flock parameters: alignment, cohesion, and separation.
        // Alignment: determines which direction to move
        // Cohesion: Determines a common position to move towards
        // Separation: Determines how far apart the agent is from all other agents
        private void DetermineFlockParameters(int index, out Vector3 alignment, out Vector3 cohesion, out Vector3 separation)
        {
            alignment = cohesion = separation = Vector3.zero;
            int neighborCount = 0;
            var agentPosition = m_Transforms[index].position;
            // Loop through each agent to determine the alignment, cohesion, and separation
            for (int i = 0; i < m_Agents.Length; ++i) {
                // The agent can't compare against itself
                if (index != i) {
                    var position = m_Transforms[i].position;
                    // Only determine the parameters if the other agent is its neighbor
                    if (Vector3.Magnitude(position - agentPosition) < m_NeighborDistance.Value) {
                        // This agent is the neighbor of the original agent so add the alignment, cohesion, and separation
                        alignment += Velocity(i);
                        cohesion += position;
                        separation += position - agentPosition;
                        neighborCount++;
                    }
                }
            }

            // Don't move if there are no neighbors
            if (neighborCount == 0) {
                return;
            }
            // Normalize all of the values
            alignment = (alignment / neighborCount).normalized;
            cohesion = ((cohesion / neighborCount) - agentPosition).normalized;
            separation = ((separation / neighborCount) * -1).normalized;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_NeighborDistance = 100;
            m_LookAheadDistance = 5;
            m_AlignmentWeight = 0.4f;
            m_CohesionWeight = 0.5f;
            m_SeparationWeight = 0.6f;
        }
    }
}