using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(RelativeSpeed))]
    [CanEditMultipleObjects]
    class RelativeSpeedEditor : Editor
    {
        SerializedProperty walkCurve, strafeCurve, runCurve, jumpImpulseCurve, gravityStrengthCurve;

        private Rect quickSetupRect;

        void OnEnable()
        {
            walkCurve = serializedObject.FindProperty("walkCurve");
            strafeCurve = serializedObject.FindProperty("strafeCurve");
            runCurve = serializedObject.FindProperty("runCurve");
            jumpImpulseCurve = serializedObject.FindProperty("jumpImpulseCurve");
            gravityStrengthCurve = serializedObject.FindProperty("gravityCurve");
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpEditor.UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            serializedObject.Update();

            if (EditorGUILayout.DropdownButton(new GUIContent("Quick Setup"), FocusType.Keyboard))
            {
                var popup = new ProportionalSetupPopup(
                    serializedObject, quickSetupRect.width,

                    new ProportionalSetupPopup.CurveProperty(
                        "walkCurve",
                        "Walk Speed",
                        2f, false,
                        "Walk speed for an avatar at the base eye height."
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "strafeCurve",
                        "Strafe Speed",
                        2f, false,
                        "Strafe speed for an avatar at the base eye height."
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "runCurve",
                        "Run Speed",
                        4f, false,
                        "Run speed for an avatar at the base eye height."
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "jumpImpulseCurve",
                        "Jump Impulse",
                        3f, false,
                        "Jump impulse for an avatar at the base eye height."
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "gravityCurve",
                        null,
                        1f, false,
                        null
                    ));

                PopupWindow.Show(quickSetupRect, popup);
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

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayField(SerializedProperty field, string label, string tooltip)
        {
            EditorGUILayout.PropertyField(field, new GUIContent(label, tooltip));
        }
    }
}