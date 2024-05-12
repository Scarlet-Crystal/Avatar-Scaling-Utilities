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
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpEditor.UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            serializedObject.Update();

            collisionAction.DisplayActionSettings(
                "Collision Action",
                "Action to perfom when a particle hits the player."
            );

            serializedObject.ApplyModifiedProperties();
        }
    }
}