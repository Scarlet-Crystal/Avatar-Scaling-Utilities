using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(ScalingZone))]
    [CanEditMultipleObjects]
    class ScalingZoneEditor : Editor
    {
        private ActionSettings insideAction, leaveAction;

        void OnEnable()
        {
            insideAction = new ActionSettings(serializedObject, "inside");
            leaveAction = new ActionSettings(serializedObject, "leave");

            // Aug 12, 2023, Unity 2019.4.31f1 - Undo doesn't seem to properly invalidate
            //  the serializedObject's different cache for enum fields.
            Undo.undoRedoPerformed += serializedObject.SetIsDifferentCacheDirty;
        }

        void OnDisable()
        {
            Undo.undoRedoPerformed -= serializedObject.SetIsDifferentCacheDirty;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            insideAction.DisplayActionSettings(
                "Inside Action",
                "Action to perform when the player enters the trigger. If User Scaling Limit is set to Restrict"
                + ", then this action is also performed when the player changes avatars while inside the trigger.");

            EditorGUILayout.Space();

            leaveAction.DisplayActionSettings(
                "Leave Action",
                "Action to perform when the player leaves the trigger.");

            serializedObject.ApplyModifiedProperties();
        }
    }
}