using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    class QuickSetupPopup : PopupWindowContent
    {
        private float baseEyeHeight = 1.5f, walkSpeed = 2f, strafeSpeed = 2f, runSpeed = 4f, jumpImpulse = 3f;
        private float voiceRange = 25f, voiceGain = 15f;

        private readonly float popupWidth;
        private readonly SerializedObject setupTarget;

        public QuickSetupPopup(SerializedObject setupTarget, float popupWidth)
        {
            this.popupWidth = popupWidth;
            this.setupTarget = setupTarget;
        }

        public override void OnGUI(Rect _)
        {
            DisplayFloatField(ref baseEyeHeight, "Base Eye Height", "The eye height the baseline setting are configured for.");
            DisplayFloatField(ref walkSpeed, "Walk Speed", "Walk speed for an avatar at the base eye height.");
            DisplayFloatField(ref strafeSpeed, "Strafe Speed", "Strafe speed for an avatar at the base eye height.");
            DisplayFloatField(ref runSpeed, "Run Speed", "Run speed for an avatar at the base eye height.");
            DisplayFloatField(ref jumpImpulse, "Jump Impulse", "Jump impulse for an avatar at the base eye height.");
            DisplayFloatField(ref voiceRange, "Voice Range", "Voice range for an avatar at the base eye height.");
            DisplayFloatField(ref voiceGain, "Voice Gain", "Sets the voice gain irrespective of avatar height.");

            EditorGUILayout.Space();
            if (GUILayout.Button("Apply"))
            {
                setupTarget.Update();

                SetupCurve(setupTarget.FindProperty("walkCurve"), walkSpeed, baseEyeHeight);
                SetupCurve(setupTarget.FindProperty("strafeCurve"), strafeSpeed, baseEyeHeight);
                SetupCurve(setupTarget.FindProperty("runCurve"), runSpeed, baseEyeHeight);
                SetupCurve(setupTarget.FindProperty("jumpImpulseCurve"), jumpImpulse, baseEyeHeight);
                SetupCurve(setupTarget.FindProperty("gravityCurve"), 1f, baseEyeHeight);

                SetupCurve(setupTarget.FindProperty("voiceFarCurve"), voiceRange, baseEyeHeight);
                SetupConstantCurve(setupTarget.FindProperty("voiceNearCurve"), 0f);
                SetupConstantCurve(setupTarget.FindProperty("voiceVolumetricRadiusCurve"), 0.005f);
                SetupConstantCurve(setupTarget.FindProperty("voiceGainCurve"), voiceGain);

                setupTarget.ApplyModifiedProperties();
                editorWindow.Close();
            }
        }

        public override Vector2 GetWindowSize() => new Vector2(popupWidth, 172f);

        private void SetupCurve(SerializedProperty sp, float baseline, float baseHeight)
        {
            sp.animationCurveValue = AnimationCurve.Linear(0f, 0f, 100f, 100f * (baseline / baseHeight));
        }

        private void SetupConstantCurve(SerializedProperty sp, float value)
        {
            sp.animationCurveValue = AnimationCurve.Constant(-1, 0, value);
        }

        private void DisplayFloatField(ref float field, string label, string tooltip)
        {
            field = EditorGUILayout.FloatField(new GUIContent(label, tooltip), field);
            field = Mathf.Max(field, 0);
        }
    }
}