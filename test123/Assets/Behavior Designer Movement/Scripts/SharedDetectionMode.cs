
namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [System.Flags]
    public enum DetectionMode : uint
    {
        Object = 1,
        ObjectList = 2,
        Tag = 4,
        LayerMask = 8
    }

    [System.Serializable]
    public class SharedDetectionMode : SharedVariable<DetectionMode>
    {
        public static implicit operator SharedDetectionMode(DetectionMode value) { return new SharedDetectionMode { Value = value }; }
    }
}
