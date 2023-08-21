using UnityEditor;

namespace AvatarScalingUtilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScalingStation))]
    class ScalingStationEditor : ScalingGuardEditor
    {
        protected override string StartLabel => "Entered Action";
        protected override string StartTooltip =>
            "Action to perform when the player enters the station. If User Scaling Limit is set to Restrict"
            + ", then this action is also performed when the player changes avatars while in the station.";

        protected override string StopLabel => "Exited Action";
        protected override string StopTooltip => "Action to perform when the player exits the station.";
    }
}