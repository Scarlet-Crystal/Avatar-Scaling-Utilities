using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    class ProportionalSetupPopup : PopupWindowContent
    {
        public class CurveProperty
        {
            public string serializedName;
            public string label;
            public string tooltip;
            public float value;
            public bool setupConstantCurve;

            public CurveProperty(string serializedName, string label, float value, bool setupConstantCurve, string tooltip)
            {
                this.serializedName = serializedName;
                this.label = label;
                this.value = value;
                this.setupConstantCurve = setupConstantCurve;
                this.tooltip = tooltip;
            }
        }

        private float baseEyeHeight = 1.5f;
        private readonly CurveProperty[] properties;

        private readonly float popupWidth;
        private readonly SerializedObject setupTarget;

        public ProportionalSetupPopup(SerializedObject setupTarget, float popupWidth, params CurveProperty[] properties)
        {
            this.popupWidth = popupWidth;
            this.setupTarget = setupTarget;
            this.properties = properties;
        }

        public override void OnGUI(Rect _)
        {
            DisplayFloatField(ref baseEyeHeight, "Base Eye Height", "The eye height the baseline setting are configured for.");

            foreach (CurveProperty prop in properties)
            {
                if (!string.IsNullOrEmpty(prop.label))
                {
                    DisplayFloatField(ref prop.value, prop.label, prop.tooltip);
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Apply"))
            {
                setupTarget.Update();

                foreach (CurveProperty prop in properties)
                {
                    if (prop.setupConstantCurve)
                    {
                        SetupConstantCurve(setupTarget.FindProperty(prop.serializedName), prop.value);
                    }
                    else
                    {
                        SetupCurve(setupTarget.FindProperty(prop.serializedName), prop.value, baseEyeHeight);
                    }
                }

                setupTarget.ApplyModifiedProperties();
                editorWindow.Close();
            }
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(popupWidth, 52f + properties.Where((p) => p.label != null).Count() * 20);
        }

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