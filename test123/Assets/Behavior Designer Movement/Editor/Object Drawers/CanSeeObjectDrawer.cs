using UnityEngine;
using BehaviorDesigner.Editor;
using System.Reflection;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks.Movement.Editor.ObjectDrawers
{
    [CustomObjectDrawer(typeof(CanSeeObject))]
    public class CanSeeObjectDrawer : ObjectDrawer
    {
        private CanSeeObject m_PrevCanSeeObject;
        private Dictionary<string, MovementObjectDrawerUtility.FieldContent> m_FieldContentByField = new Dictionary<string, MovementObjectDrawerUtility.FieldContent>();

        public override void OnGUI(GUIContent label)
        {
            var canSeeObject = task as CanSeeObject;

            if (m_PrevCanSeeObject == null || canSeeObject != m_PrevCanSeeObject) {
                m_PrevCanSeeObject = canSeeObject;
                m_FieldContentByField.Clear();

                var fields = canSeeObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (var field in fields) {
                    m_FieldContentByField.Add(field.Name, new MovementObjectDrawerUtility.FieldContent { 
                                                                Field = field, 
                                                                Name = BehaviorDesignerUtility.SplitCamelCase(field.Name),
                                                                Tooltip = MovementObjectDrawerUtility.GetTooltip(task, field.Name)
                    });
                }
            }

            var fieldContent = m_FieldContentByField["m_UsePhysics2D"];
            canSeeObject.m_UsePhysics2D = (bool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_UsePhysics2D);
            fieldContent = m_FieldContentByField["m_DetectionMode"];
            canSeeObject.m_DetectionMode = (SharedDetectionMode)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_DetectionMode);
            if (GUI.changed) { // 2022.3 throws an exception if a flag field is changed. Handle it here.
                BehaviorDesignerWindow.instance.SaveBehavior();
            }

            var detectionMode = canSeeObject.m_DetectionMode.Value;
            if ((detectionMode & DetectionMode.Object) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObject"];
                canSeeObject.m_TargetObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetObject);
            }
            if ((detectionMode & DetectionMode.ObjectList) != 0) {
                fieldContent = m_FieldContentByField["m_TargetObjects"];
                canSeeObject.m_TargetObjects= (SharedGameObjectList)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetObjects);
            }
            if ((detectionMode & DetectionMode.Tag) != 0) {
                fieldContent = m_FieldContentByField["m_TargetTag"];
                canSeeObject.m_TargetTag = (SharedString)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetTag);
            }
            if ((detectionMode & DetectionMode.LayerMask) != 0) {
                fieldContent = m_FieldContentByField["m_TargetLayerMask"];
                canSeeObject.m_TargetLayerMask = (SharedLayerMask)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetLayerMask);
                fieldContent = m_FieldContentByField["m_MaxCollisionCount"];
                canSeeObject.m_MaxCollisionCount = (int)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_MaxCollisionCount);
            }

            fieldContent = m_FieldContentByField["m_IgnoreLayerMask"];
            canSeeObject.m_IgnoreLayerMask = (LayerMask)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_IgnoreLayerMask);
            fieldContent = m_FieldContentByField["m_FieldOfViewAngle"];
            canSeeObject.m_FieldOfViewAngle = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_FieldOfViewAngle);
            fieldContent = m_FieldContentByField["m_ViewDistance"];
            canSeeObject.m_ViewDistance = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_ViewDistance);
            fieldContent = m_FieldContentByField["m_Offset"];
            canSeeObject.m_Offset = (SharedVector3)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_Offset);
            fieldContent = m_FieldContentByField["m_TargetOffset"];
            canSeeObject.m_TargetOffset = (SharedVector3)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetOffset);

            if (canSeeObject.m_UsePhysics2D) {
                fieldContent = m_FieldContentByField["m_AngleOffset2D"];
                canSeeObject.m_AngleOffset2D = (SharedFloat)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_AngleOffset2D);
            }
            fieldContent = m_FieldContentByField["m_UseTargetBone"];
            canSeeObject.m_UseTargetBone = (SharedBool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_UseTargetBone);
            if (canSeeObject.m_UseTargetBone.Value) {
                fieldContent = m_FieldContentByField["m_TargetBone"];
                canSeeObject.m_TargetBone = (SharedHumanBodyBones)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_TargetBone);
            }
            fieldContent = m_FieldContentByField["m_DrawDebugRay"];
            canSeeObject.m_DrawDebugRay = (SharedBool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_DrawDebugRay);
            fieldContent = m_FieldContentByField["m_DisableAgentColliderLayer"];
            canSeeObject.m_DisableAgentColliderLayer = (SharedBool)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_DisableAgentColliderLayer);
            fieldContent = m_FieldContentByField["m_ReturnedObject"];
            canSeeObject.m_ReturnedObject = (SharedGameObject)FieldInspector.DrawField(task, fieldContent.GetGUIContent(), fieldContent.Field, canSeeObject.m_ReturnedObject);
        }
    }
}