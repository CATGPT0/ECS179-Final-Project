using UnityEngine;
using BehaviorDesigner.Editor;
using System.Reflection;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks.Movement.Editor.ObjectDrawers
{
    [CustomObjectDrawer(typeof(CanHearObject))]
    public class CanHearObjectDrawer : ObjectDrawer
    {
        private CanHearObject m_PrevCanHearObject;
        private Dictionary<string, MovementObjectDrawerUtility.FieldContent> m_FieldContentByField = new Dictionary<string, MovementObjectDrawerUtility.FieldContent>();

        public override void OnGUI(GUIContent label)
        {
            var canHearObject = task as CanHearObject;

            if (m_PrevCanHearObject == null || canHearObject != m_PrevCanHearObject) {
                m_PrevCanHearObject = canHearObject;
                m_FieldContentByField.Clear();

                var fields = canHearObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (var field in fields) {
                    m_FieldContentByField.Add(field.Name, new MovementObjectDrawerUtility.FieldContent { 
                                                                Field = field, 
                                                                Name = BehaviorDesignerUtility.SplitCamelCase(field.Name),
                                                                Tooltip = MovementObjectDrawerUtility.GetTooltip(task, field.Name)
                    });
                }
            }

            var fieldContent = m_FieldContentByField["m_UsePhysics2D"];
            canHearObject.m_UsePhysics2D = (bool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_UsePhysics2D);
            fieldContent = m_FieldContentByField["m_DetectionMode"];
            canHearObject.m_DetectionMode = (SharedDetectionMode)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_DetectionMode);
            if (GUI.changed) { // 2022.3 throws an exception if a flag field is changed. Handle it here.
                BehaviorDesignerWindow.instance.SaveBehavior();
            }

            var detectionMode = canHearObject.m_DetectionMode.Value;
            if ((detectionMode & DetectionMode.Object) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObject"];
                canHearObject.m_TargetObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_TargetObject);
            }
            if ((detectionMode & DetectionMode.ObjectList) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObjects"];
                canHearObject.m_TargetObjects= (SharedGameObjectList)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_TargetObjects);
            }
            if ((detectionMode & DetectionMode.Tag) != 0) {
                fieldContent = m_FieldContentByField["m_TargetTag"];
                canHearObject.m_TargetTag = (SharedString)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_TargetTag);
            }
            if ((detectionMode & DetectionMode.LayerMask) != 0) {
                fieldContent = m_FieldContentByField["m_TargetLayerMask"];
                canHearObject.m_TargetLayerMask = (SharedLayerMask)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_TargetLayerMask);
                fieldContent = m_FieldContentByField["m_MaxCollisionCount"];
                canHearObject.m_MaxCollisionCount = (int)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_MaxCollisionCount);
            }

            fieldContent = m_FieldContentByField["m_HearingRadius"];
            canHearObject.m_HearingRadius = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_HearingRadius);
            fieldContent = m_FieldContentByField["m_AudibilityThreshold"];
            canHearObject.m_AudibilityThreshold = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_AudibilityThreshold);
            fieldContent = m_FieldContentByField["m_Offset"];
            canHearObject.m_Offset = (SharedVector3)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_Offset);
            
            fieldContent = m_FieldContentByField["m_ReturnedObject"];
            canHearObject.m_ReturnedObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canHearObject.m_ReturnedObject);
        }
    }
}