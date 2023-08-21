using UnityEditor;

namespace AvatarScalingUtilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScalingTrigger))]
    class ScalingTriggerEditor : ScalingGuardEditor
    {
        protected override string StartLabel => "Inside Action";
        protected override string StartTooltip => 
            "Action to perform when the player enters the trigger. If User Scaling Limit is set to Restrict" 
            + ", then this action is also performed when the player changes avatars while inside the trigger.";
            
        protected override string StopLabel => "Leave Action";
        protected override string StopTooltip => "Action to perform when the player leaves the trigger.";
    }
}