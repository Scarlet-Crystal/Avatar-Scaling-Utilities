using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    abstract class ScalingGuardEditor : Editor
    {
        private ActionSettings startAction, stopAction;

        protected abstract string StartLabel { get; }
        protected abstract string StartTooltip { get; }
        protected abstract string StopLabel { get; }
        protected abstract string StopTooltip { get; }

        void OnEnable()
        {
            startAction = new ActionSettings(serializedObject, "start");
            stopAction = new ActionSettings(serializedObject, "stop");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            startAction.DisplayActionSettings(StartLabel, StartTooltip);

            EditorGUILayout.Space();

            stopAction.DisplayActionSettings(StopLabel, StopTooltip);


            serializedObject.ApplyModifiedProperties();
        }
    }
}