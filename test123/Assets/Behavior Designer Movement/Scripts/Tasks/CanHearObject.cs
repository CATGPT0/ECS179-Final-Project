using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Check to see if the any objects are within hearing range of the current agent.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("a464405df8e82b24db602534724b5e6f", "941bd88188259374d885440560f1a29d")]
    public class CanHearObject : Conditional
    {
        [Tooltip("Should the 2D version be used?")]
        [UnityEngine.Serialization.FormerlySerializedAs("usePhysics2D")]
        public bool m_UsePhysics2D;
        [Tooltip("Specifies the type of detection that should be used.")]
        public SharedDetectionMode m_DetectionMode = DetectionMode.Object | DetectionMode.ObjectList | DetectionMode.Tag | DetectionMode.LayerMask;
        [Tooltip("The object that we are searching for")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetObject")]
        public SharedGameObject m_TargetObject;
        [Tooltip("The objects that we are searching for")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetObjects")]
        public SharedGameObjectList m_TargetObjects;
        [Tooltip("The tag of the object that we are searching for")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetTag")]
        public SharedString m_TargetTag;
        [Tooltip("The LayerMask of the objects that we are searching for")]
        [UnityEngine.Serialization.FormerlySerializedAs("objectLayerMask")]
        public SharedLayerMask m_TargetLayerMask;
        [Tooltip("If using the object layer mask, specifies the maximum number of colliders that the physics cast can collide with")]
        [UnityEngine.Serialization.FormerlySerializedAs("maxCollisionCount")]
        public int m_MaxCollisionCount = 200;
        [Tooltip("How far away the unit can hear")]
        [UnityEngine.Serialization.FormerlySerializedAs("hearingRadius")]
        public SharedFloat m_HearingRadius = 50;
        [Tooltip("The further away a sound source is the less likely the agent will be able to hear it. " +
                 "Set a threshold for the the minimum audibility level that the agent can hear")]
        [UnityEngine.Serialization.FormerlySerializedAs("audibilityThreshold")]
        public SharedFloat m_AudibilityThreshold = 0.05f;
        [Tooltip("The hearing offset relative to the pivot position")]
        [UnityEngine.Serialization.FormerlySerializedAs("offset")]
        public SharedVector3 m_Offset;
        [Tooltip("The returned object that is heard")]
        [UnityEngine.Serialization.FormerlySerializedAs("returnedObject")]
        public SharedGameObject m_ReturnedObject;

        private Collider[] m_OverlapColliders;
        private Collider2D[] m_Overlap2DColliders;

        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate()
        {
            m_ReturnedObject.Value = null;
            if ((m_DetectionMode.Value & DetectionMode.Object) != 0 && m_TargetObject.Value != null) {
                var target = m_TargetObject.Value;
                if (Vector3.Distance(target.transform.position, transform.position) < m_HearingRadius.Value) {
                    m_ReturnedObject.Value = MovementUtility.WithinHearingRange(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_TargetObject.Value);
                }
            }

            if (m_ReturnedObject.Value == null && (m_DetectionMode.Value & DetectionMode.ObjectList) != 0) {
                GameObject objectFound = null;
                for (int i = 0; i < m_TargetObjects.Value.Count; ++i) {
                    var audibility = 0f;
                    GameObject obj;
                    if (Vector3.Distance(m_TargetObjects.Value[i].transform.position, transform.position) < m_HearingRadius.Value) {
                        if ((obj = MovementUtility.WithinHearingRange(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_TargetObjects.Value[i], ref audibility)) != null) {
                            objectFound = obj;
                        }
                    }
                }
                m_ReturnedObject.Value = objectFound;
            }

            if (m_ReturnedObject.Value == null && (m_DetectionMode.Value & DetectionMode.Tag) != 0 && !string.IsNullOrEmpty(m_TargetTag.Value)) {
                var targets = GameObject.FindGameObjectsWithTag(m_TargetTag.Value);
                if (targets != null) {
                    for (int i = 0; i < targets.Length; ++i) {
                        var audibility = 0f;
                        GameObject obj;
                        if (Vector3.Distance(targets[i].transform.position, transform.position) < m_HearingRadius.Value) {
                            if ((obj = MovementUtility.WithinHearingRange(transform, m_Offset.Value, m_AudibilityThreshold.Value, targets[i], ref audibility)) != null) {
                                m_ReturnedObject.Value = obj;
                            }
                        }
                    }
                }
            }

            if (m_ReturnedObject.Value == null && (m_DetectionMode.Value & DetectionMode.LayerMask) != 0) {
                if (m_UsePhysics2D) {
                    if (m_Overlap2DColliders == null) {
                        m_Overlap2DColliders = new Collider2D[m_MaxCollisionCount];
                    }
                    m_ReturnedObject.Value = MovementUtility.WithinHearingRange2D(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_HearingRadius.Value, m_Overlap2DColliders, m_TargetLayerMask.Value);
                } else {
                    if (m_OverlapColliders == null) {
                        m_OverlapColliders = new Collider[m_MaxCollisionCount];
                    }
                    m_ReturnedObject.Value = MovementUtility.WithinHearingRange(transform, m_Offset.Value, m_AudibilityThreshold.Value, m_HearingRadius.Value, m_OverlapColliders, m_TargetLayerMask.Value);
                }
            }

            if (m_ReturnedObject.Value != null) {
                // m_ReturnedObject success if an object was heard
                return TaskStatus.Success;
            }

            // An object is not within heard so return failure
            return TaskStatus.Failure;
        }

        // Reset the public variables
        public override void OnReset()
        {
            m_HearingRadius = 50;
            m_AudibilityThreshold = 0.05f;
        }

        // Draw the hearing radius
        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (Owner == null || m_HearingRadius == null) {
                return;
            }
            var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position, Owner.transform.up, m_HearingRadius.Value);
            UnityEditor.Handles.color = oldColor;
#endif
        }

        public override void OnBehaviorComplete()
        {
            MovementUtility.ClearCache();
        }
    }
}