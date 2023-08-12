using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(RelativeSpeedAndVoice))]
    [CanEditMultipleObjects]
    class RelativeSpeedAndVoiceEditor : Editor
    {
        SerializedProperty walkCurve, strafeCurve, runCurve, jumpImpulseCurve, gravityStrengthCurve, voiceRangeCurve;

        private Rect quickSetupRect;

        void OnEnable()
        {
            walkCurve = serializedObject.FindProperty("walkCurve");
            strafeCurve = serializedObject.FindProperty("strafeCurve");
            runCurve = serializedObject.FindProperty("runCurve");
            jumpImpulseCurve = serializedObject.FindProperty("jumpImpulseCurve");
            gravityStrengthCurve = serializedObject.FindProperty("gravityCurve");
            voiceRangeCurve = serializedObject.FindProperty("voiceRangeCurve");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (EditorGUILayout.DropdownButton(new GUIContent("Quick Setup"), FocusType.Keyboard))
            {
                var quickSetupPopup = new QuickSetupPopup(serializedObject, quickSetupRect.width);
                PopupWindow.Show(quickSetupRect, quickSetupPopup);
            }

            if (Event.current.type == EventType.Repaint)
            {
                quickSetupRect = GUILayoutUtility.GetLastRect();
            }

            DisplayField(walkCurve, "Walk Speed", "Maps the local player's eye height to the desired walk speed.");
            DisplayField(strafeCurve, "Strafe Speed", "Maps the local player's eye height to the desired strafe speed.");
            DisplayField(runCurve, "Run Speed", "Maps the local player's eye height to the desired run speed.");
            DisplayField(jumpImpulseCurve, "Jump Impulse", "Maps the local player's eye height to the desired jump impulse.");
            DisplayField(gravityStrengthCurve, "Gravity Strength", "Map the local player's eye height to the desired gravity strength.");
            DisplayField(voiceRangeCurve, "Voice Range", "Maps the remote player's eye height to the desired voice range.");

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayField(SerializedProperty field, string label, string tooltip)
        {
            EditorGUILayout.PropertyField(field, new GUIContent(label, tooltip));
        }
    }
}