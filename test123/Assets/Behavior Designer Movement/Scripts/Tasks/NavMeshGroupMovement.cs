using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    public abstract class NavMeshGroupMovement : GroupMovement
    {
        [Tooltip("All of the agents")]
        [UnityEngine.Serialization.FormerlySerializedAs("agents")]
        public SharedGameObject[] m_Agents;
        [Tooltip("The speed of the agents")]
        [UnityEngine.Serialization.FormerlySerializedAs("speed")]
        public SharedFloat m_Speed = 10;
        [Tooltip("The angular speed of the agents")]
        [UnityEngine.Serialization.FormerlySerializedAs("angularSpeed")]
        public SharedFloat m_AngularSpeed = 120;

        // A cache of the NavMeshAgents
        private NavMeshAgent[] m_NavMeshAgents;
        protected Transform[] m_Transforms;

        public override void OnStart()
        {
            m_NavMeshAgents = new NavMeshAgent[m_Agents.Length];
            m_Transforms = new Transform[m_Agents.Length];
            for (int i = 0; i < m_Agents.Length; ++i) {
                m_Transforms[i] = m_Agents[i].Value.transform;
                m_NavMeshAgents[i] = m_Agents[i].Value.GetComponent<NavMeshAgent>();
                m_NavMeshAgents[i].speed = m_Speed.Value;
                m_NavMeshAgents[i].angularSpeed = m_AngularSpeed.Value;
                m_NavMeshAgents[i].isStopped = false;
            }
        }

        protected override bool SetDestination(int index, Vector3 target)
        {
            if (m_NavMeshAgents[index].destination == target) {
                return true;
            }
            return m_NavMeshAgents[index].SetDestination(target);
        }

        protected override Vector3 Velocity(int index)
        {
            return m_NavMeshAgents[index].velocity;
        }

        public override void OnEnd()
        {
            // Disable the nav mesh
            for (int i = 0; i < m_NavMeshAgents.Length; ++i) {
                if (m_NavMeshAgents[i] != null) {
                    m_NavMeshAgents[i].isStopped = true;
                }
            }
        }

        // Reset the public variables
        public override void OnReset()
        {
            m_Agents = null;
        }
    }
}