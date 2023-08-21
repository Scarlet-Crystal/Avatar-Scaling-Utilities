using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(RelativeVoice))]
    [CanEditMultipleObjects]
    class RelativeVoiceEditor : Editor
    {
        SerializedProperty voiceFarCurve, voiceNearCurve, voiceVolumetricRadiusCurve, voiceGainCurve;

        private Rect quickSetupRect;

        void OnEnable()
        {
            voiceFarCurve = serializedObject.FindProperty("voiceFarCurve");
            voiceNearCurve = serializedObject.FindProperty("voiceNearCurve");
            voiceVolumetricRadiusCurve = serializedObject.FindProperty("voiceVolumetricRadiusCurve");
            voiceGainCurve = serializedObject.FindProperty("voiceGainCurve");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (EditorGUILayout.DropdownButton(new GUIContent("Quick Setup"), FocusType.Keyboard))
            {
                var popup = new ProportionalSetupPopup(
                    serializedObject, quickSetupRect.width,

                    new ProportionalSetupPopup.CurveProperty(
                        "voiceFarCurve",
                        "Voice Range",
                        25f, false,
                        "Voice range for an avatar at the base eye height."),

                    new ProportionalSetupPopup.CurveProperty(
                        "voiceGainCurve",
                        "Voice Gain",
                        15f, true,
                        "Sets the voice gain irrespective of avatar height."
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "voiceNearCurve",
                        null,
                        0f, true,
                        null
                    ),

                    new ProportionalSetupPopup.CurveProperty(
                        "voiceVolumetricRadiusCurve",
                        null,
                        0.005f, true, //Aug 3rd 2023 - Setting this to a small, nonzero value fixes this bug: https://feedback.vrchat.com/open-beta/p/1314-stereo-separation-for-positional-audio-is-not-corrected-with-scale
                        null
                    )
                );

                PopupWindow.Show(quickSetupRect, popup);
            }

            if (Event.current.type == EventType.Repaint)
            {
                quickSetupRect = GUILayoutUtility.GetLastRect();
            }

            DisplayField(voiceFarCurve, "Far Range", "Maps the remote player's eye height to the desired voice far range.");
            DisplayField(voiceNearCurve, "Near Range", "Maps the remote player's eye height to the desired voice near range.");
            DisplayField(voiceVolumetricRadiusCurve, "Volumetric Radius", "Maps the remote player's eye height to the desired volumetic radius.");
            DisplayField(voiceGainCurve, "Gain", "Maps the remote player's eye height to the desired voice gain.");

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayField(SerializedProperty field, string label, string tooltip)
        {
            EditorGUILayout.PropertyField(field, new GUIContent(label, tooltip));
        }
    }
}