using System;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    class ActionSettings
    {
        private SerializedProperty action, limit, a, b, curve;

        private const string actionDesctiption =
@"

None - Take no action

Set Eye Height - Set eye height to a specific height.

Limit Eye Height - Limits the eye height to a specified range. If the current eye height is outside of said range, a random eye height within the range will be set.

Remap Eye Height - Transforms the eye height with a specified curve.

Remap And Limit Eye Height - Transforms the eye height with a specified curve, then limits it to a specified range. If the resulting eye height is outside of said range, a random eye height within the range will be set.

Multiply Prefab Height - Set the eye height to a multiple of the player's avatar's original eye height.";


        private const string limitDescrition =
@"Whether or not to restrict manual scaling.

Keep - Preserve the current settings.

Restrict - Either disables manual scaling, or limits the range the player can scale if the action is set to Limit Eye Height or Remap and Limit Eye Height.

Reset - Fully unlocks manual scaling.";


        public ActionSettings(SerializedObject so, string prefix)
        {
            action = so.FindProperty($"{prefix}Action");
            limit = so.FindProperty($"{prefix}Limit");
            a = so.FindProperty($"{prefix}A");
            b = so.FindProperty($"{prefix}B");
            curve = so.FindProperty($"{prefix}Curve");
        }

        public void DisplayActionSettings(string label, string tooltip)
        {
            int lastSelected = -1;
            if (!action.hasMultipleDifferentValues)
            {
                lastSelected = action.enumValueIndex;
            }

            DisplayField(action, label, tooltip + actionDesctiption);
            EditorGUI.indentLevel++;

            if (!action.hasMultipleDifferentValues)
            {
                int selected = action.enumValueIndex;

                var selectedAction = Enum.Parse<ScalingActions>(action.enumNames[selected]);

                if (lastSelected != selected)
                {
                    switch (selectedAction)
                    {
                        case ScalingActions.SetEyeHeight:
                            a.floatValue = 1.6f;
                            break;

                        case ScalingActions.LimitEyeHeight:
                            a.floatValue = 0.1f;
                            b.floatValue = 100f;
                            break;

                        case ScalingActions.MultiplyPrefabHeight:
                            a.floatValue = 1f;
                            break;

                        case ScalingActions.RemapAndLimitEyeHeight:
                            a.floatValue = 0.1f;
                            b.floatValue = 100f;
                            break;

                        default:
                            break;
                    }
                }

                switch (selectedAction)
                {
                    case ScalingActions.None:
                        break;

                    case ScalingActions.SetEyeHeight:
                        DisplayField(a, "Height", "Eye height to scale the player to.");
                        break;

                    case ScalingActions.LimitEyeHeight:
                        DisplayField(a, "Min Height", "The lowest eye height to allow.");
                        DisplayField(b, "Max Height", "The heighest eye height to allow.");
                        break;

                    case ScalingActions.MultiplyPrefabHeight:
                        DisplayField(a, "Multiplier", "The amount to multiply the avatar's original height with.");
                        break;

                    case ScalingActions.RemapEyeHeight:
                        DisplayField(curve, "Remap Curve", "Maps the current eye height to the desired eye height.");
                        break;

                    case ScalingActions.RemapAndLimitEyeHeight:
                        DisplayField(a, "Min Height", "The lowest eye height to allow.");
                        DisplayField(b, "Max Height", "The heighest eye height to allow.");
                        DisplayField(curve, "Remap Curve", "Maps the current eye height to the desired eye height.");
                        break;
                }

                if (!a.hasMultipleDifferentValues && !b.hasMultipleDifferentValues)
                {
                    a.floatValue = Mathf.Max(0, a.floatValue);
                    b.floatValue = Mathf.Max(a.floatValue + 0.1f, b.floatValue);
                }
            }

            DisplayField(limit, "User Scaling Limits", limitDescrition);

            EditorGUI.indentLevel--;
        }

        private void DisplayField(SerializedProperty field, string label, string tooltip)
        {
            EditorGUILayout.PropertyField(field, new GUIContent(label, tooltip));
        }
    }
}