using UnityEngine;
using BehaviorDesigner.Editor;
using System.Reflection;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks.Movement.Editor.ObjectDrawers
{
    [CustomObjectDrawer(typeof(WithinDistance))]
    public class WithinDistanceDrawer : ObjectDrawer
    {
        private WithinDistance m_PrevWithinDistance;
        private Dictionary<string, MovementObjectDrawerUtility.FieldContent> m_FieldContentByField = new Dictionary<string, MovementObjectDrawerUtility.FieldContent>();

        public override void OnGUI(GUIContent label)
        {
            var withinDistance = task as WithinDistance;

            if (m_PrevWithinDistance == null || withinDistance != m_PrevWithinDistance) {
                m_PrevWithinDistance = withinDistance;
                m_FieldContentByField.Clear();

                var fields = withinDistance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (var field in fields) {
                    m_FieldContentByField.Add(field.Name, new MovementObjectDrawerUtility.FieldContent { 
                                                                Field = field, 
                                                                Name = BehaviorDesignerUtility.SplitCamelCase(field.Name),
                                                                Tooltip = MovementObjectDrawerUtility.GetTooltip(task, field.Name)
                    });
                }
            }

            var fieldContent = m_FieldContentByField["m_UsePhysics2D"];
            withinDistance.m_UsePhysics2D = (bool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_UsePhysics2D);
            fieldContent = m_FieldContentByField["m_DetectionMode"];
            withinDistance.m_DetectionMode = (SharedDetectionMode)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_DetectionMode);
            if (GUI.changed) { // 2022.3 throws an exception if a flag field is changed. Handle it here.
                BehaviorDesignerWindow.instance.SaveBehavior();
            }

            var detectionMode = withinDistance.m_DetectionMode.Value;
            if ((detectionMode & DetectionMode.Object) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObject"];
                withinDistance.m_TargetObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_TargetObject);
            }
            if ((detectionMode & DetectionMode.ObjectList) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObjects"];
                withinDistance.m_TargetObjects= (SharedGameObjectList)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_TargetObjects);
            }
            if ((detectionMode & DetectionMode.Tag) != 0) {
                fieldContent = m_FieldContentByField["m_TargetTag"];
                withinDistance.m_TargetTag = (SharedString)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_TargetTag);
            }
            if ((detectionMode & DetectionMode.LayerMask) != 0) {
                fieldContent = m_FieldContentByField["m_TargetLayerMask"];
                withinDistance.m_TargetLayerMask = (SharedLayerMask)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_TargetLayerMask);
                fieldContent = m_FieldContentByField["m_MaxCollisionCount"];
                withinDistance.m_MaxCollisionCount = (int)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_MaxCollisionCount);
            }

            fieldContent = m_FieldContentByField["m_Magnitude"];
            withinDistance.m_Magnitude = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_Magnitude);
            fieldContent = m_FieldContentByField["m_LineOfSight"];
            withinDistance.m_LineOfSight = (SharedBool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_LineOfSight);

            if (withinDistance.m_LineOfSight.Value) {
                fieldContent = m_FieldContentByField["m_IgnoreLayerMask"];
                withinDistance.m_IgnoreLayerMask = (LayerMask)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_IgnoreLayerMask);
            }
            fieldContent = m_FieldContentByField["m_Offset"];
            withinDistance.m_Offset = (SharedVector3)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_Offset);
            fieldContent = m_FieldContentByField["m_TargetOffset"];
            withinDistance.m_TargetOffset = (SharedVector3)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_TargetOffset);

            fieldContent = m_FieldContentByField["m_DrawDebugRay"];
            withinDistance.m_DrawDebugRay = (SharedBool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_DrawDebugRay);
            fieldContent = m_FieldContentByField["m_ReturnedObject"];
            withinDistance.m_ReturnedObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, withinDistance.m_ReturnedObject);
        }
    }
}