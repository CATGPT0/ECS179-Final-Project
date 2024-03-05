using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Patrol around the specified waypoints using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("9db06eafffd691549994cfe903905580", "3c16815a0806b2a4c8cd693c5139b3ea")]
    public class Patrol : NavMeshMovement
    {
        [Tooltip("Should the agent patrol the waypoints randomly?")]
        [UnityEngine.Serialization.FormerlySerializedAs("randomPatrol")]
        public SharedBool m_RandomPatrol;
        [Tooltip("The length of time that the agent should pause when arriving at a waypoint")]
        [UnityEngine.Serialization.FormerlySerializedAs("waypointPauseDuration")]
        public SharedFloat m_WaypointPauseDuration;
        [Tooltip("The waypoints to move to")]
        [UnityEngine.Serialization.FormerlySerializedAs("waypoints")]
        public SharedGameObjectList m_Waypoints;

        // The current index that we are heading towards within the waypoints array
        private int m_WaypointIndex;
        private float m_WaypointReachedTime;

        public override void OnStart()
        {
            base.OnStart();

            // initially move towards the closest waypoint
            float distance = Mathf.Infinity;
            float localDistance;
            for (int i = 0; i < m_Waypoints.Value.Count; ++i) {
                if ((localDistance = Vector3.Magnitude(transform.position - m_Waypoints.Value[i].transform.position)) < distance) {
                    distance = localDistance;
                    m_WaypointIndex = i;
                }
            }
            m_WaypointReachedTime = -1;
            SetDestination(Target());
        }

        // Patrol around the different waypoints specified in the waypoint array. Always return a task status of running. 
        public override TaskStatus OnUpdate()
        {
            if (m_Waypoints.Value.Count == 0) {
                return TaskStatus.Failure;
            }
            if (HasArrived()) {
                if (m_WaypointReachedTime == -1) {
                    m_WaypointReachedTime = Time.time;
                }
                // wait the required duration before switching waypoints.
                if (m_WaypointReachedTime + m_WaypointPauseDuration.Value <= Time.time) {
                    if (m_RandomPatrol.Value) {
                        if (m_Waypoints.Value.Count == 1) {
                            m_WaypointIndex = 0;
                        } else {
                            // prevent the same waypoint from being selected
                            var newWaypointIndex = m_WaypointIndex;
                            while (newWaypointIndex == m_WaypointIndex) {
                                newWaypointIndex = Random.Range(0, m_Waypoints.Value.Count);
                            }
                            m_WaypointIndex = newWaypointIndex;
                        }
                    } else {
                        m_WaypointIndex = (m_WaypointIndex + 1) % m_Waypoints.Value.Count;
                    }
                    SetDestination(Target());
                    m_WaypointReachedTime = -1;
                }
            }

            return TaskStatus.Running;
        }

        // Return the current waypoint index position
        private Vector3 Target()
        {
            if (m_WaypointIndex >= m_Waypoints.Value.Count) {
                return transform.position;
            }
            return m_Waypoints.Value[m_WaypointIndex].transform.position;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_RandomPatrol = false;
            m_WaypointPauseDuration = 0;
            m_Waypoints = null;
        }

        // Draw a gizmo indicating a patrol 
        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (m_Waypoints == null || m_Waypoints.Value == null) {
                return;
            }
            var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.yellow;
            for (int i = 0; i < m_Waypoints.Value.Count; ++i) {
                if (m_Waypoints.Value[i] != null) {
                    UnityEditor.Handles.SphereHandleCap(0, m_Waypoints.Value[i].transform.position, m_Waypoints.Value[i].transform.rotation, 1, EventType.Repaint);
                }
            }
            UnityEditor.Handles.color = oldColor;
#endif
        }
    }
}