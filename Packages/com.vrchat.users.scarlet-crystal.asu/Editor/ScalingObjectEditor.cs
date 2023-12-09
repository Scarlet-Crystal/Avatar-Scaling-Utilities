using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScalingObject))]
    class ScalingObjectEditor : ScalingGuardEditor
    {
        protected override string StartLabel => "Enabled Action";
        protected override string StartTooltip => 
            "Action to perform when this component and the attached GameObject are enabled. If User Scaling Limit is set to Restrict"
            + ", then this action is also performed when the player changes avatars while this object is enabled.";

        protected override string StopLabel => "Disabled Action";
        protected override string StopTooltip => "Action to perform when this component or the attached GameObject is disabled.";
    }
}