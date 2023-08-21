using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(ScalingParticles))]
    [CanEditMultipleObjects]
    class ScalingParticlesEditor : Editor
    {
        private ActionSettings collisionAction;

        void OnEnable()
        {
            collisionAction = new ActionSettings(serializedObject, "collision");
            
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
            
            collisionAction.DisplayActionSettings(
                "Collision Action",
                "Action to perfom when a particle hits the player."
            );

            serializedObject.ApplyModifiedProperties();
        }
    }
}