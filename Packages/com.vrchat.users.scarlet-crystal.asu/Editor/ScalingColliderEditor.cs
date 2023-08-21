using UnityEditor;

namespace AvatarScalingUtilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScalingCollider))]
    class ScalingColliderEditor : ScalingGuardEditor
    {
        protected override string StartLabel => "Start Action";
        protected override string StartTooltip =>
            "Action to perform when the player begins colliding with this object. If User Scaling Limit is set to Restrict"
            + ", then this action is also performed when the player changes avatars colliding with this object.";

        protected override string StopLabel => "Stop Action";
        protected override string StopTooltip => "Action to perform when the player stops colliding with this object.";
    }
}