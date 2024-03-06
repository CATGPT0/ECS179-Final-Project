using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Pursue the target specified using the Unity NavMesh.")]
    [TaskCategory("Movement")]
    [HelpURL("https://www.opsive.com/support/documentation/behavior-designer-movement-pack/")]
    [TaskIcon("e1387dd5098c1f7449253a17b3b39784", "c29eb3ccdfa67a744971276c325e44ea")]
    public class Pursue : NavMeshMovement
    {
        [Tooltip("How far to predict the distance ahead of the target. Lower values indicate less distance should be predicated")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetDistPrediction")]
        public SharedFloat m_TargetDistPrediction = 20;
        [Tooltip("Multiplier for predicting the look ahead distance")]
        [UnityEngine.Serialization.FormerlySerializedAs("targetDistPredictionMult")]
        public SharedFloat m_TargetDistPredictionMult = 20;
        [Tooltip("The GameObject that the agent is pursuing")]
        [UnityEngine.Serialization.FormerlySerializedAs("target")]
        public SharedGameObject m_Target;

        // The position of the target at the last frame
        private Vector3 targetPosition;

        public override void OnStart()
        {
            base.OnStart();

            targetPosition = m_Target.Value.transform.position;
            SetDestination(Target());
        }

        // Pursue the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (HasArrived()) {
                return TaskStatus.Success;
            }

            // Target will return the predicated position
            SetDestination(Target());

            return TaskStatus.Running;
        }

        // Predict the position of the target
        private Vector3 Target()
        {
            // Calculate the current distance to the target and the current speed
            var distance = (m_Target.Value.transform.position - transform.position).magnitude;
            var speed = Velocity().magnitude;

            float futurePrediction = 0;
            // Set the future prediction to max prediction if the speed is too small to give an accurate prediction
            if (speed <= distance / m_TargetDistPrediction.Value) {
                futurePrediction = m_TargetDistPrediction.Value;
            } else {
                futurePrediction = (distance / speed) * m_TargetDistPredictionMult.Value; // the prediction should be accurate enough
            }

            // Predict the future by taking the velocity of the target and multiply it by the future prediction
            var prevTargetPosition = targetPosition;
            targetPosition = m_Target.Value.transform.position;
            return targetPosition + (targetPosition - prevTargetPosition) * futurePrediction;
        }

        // Reset the public variables
        public override void OnReset()
        {
            base.OnReset();

            m_TargetDistPrediction = 20;
            m_TargetDistPredictionMult = 20;
            m_Target = null;
        }
    }
}