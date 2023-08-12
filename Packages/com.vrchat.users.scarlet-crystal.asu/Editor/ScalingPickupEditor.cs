using UnityEngine;
using UnityEditor;
using VRC.SDKBase;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(ScalingPickup))]
    class ScalingPickupEditor : Editor
    {
        private ActionSettings grab, useStart, useEnd, drop;
        private SerializedProperty respawnOnUse, respawnOnDrop;

        void OnEnable()
        {
            grab = new ActionSettings(serializedObject, "grab");
            useStart = new ActionSettings(serializedObject, "useStart");
            useEnd = new ActionSettings(serializedObject, "useEnd");
            drop = new ActionSettings(serializedObject, "drop");

            respawnOnUse = serializedObject.FindProperty("respawnOnUse");
            respawnOnDrop = serializedObject.FindProperty("respawnOnDrop");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var pickup = (target as ScalingPickup).GetComponent<VRC_Pickup>();

            bool showUseOptions = pickup.AutoHold == VRC_Pickup.AutoHoldMode.AutoDetect;
            showUseOptions = showUseOptions && pickup.orientation != VRC_Pickup.PickupOrientation.Any;
            showUseOptions = showUseOptions || pickup.AutoHold == VRC_Pickup.AutoHoldMode.Yes;

            if (showUseOptions)
            {
                EditorGUILayout.PropertyField(respawnOnUse);
            }
            else
            {
                EditorGUILayout.HelpBox("Enable autohold on the attached VRC Pickup script to reveal additional options.", MessageType.Info);
            }

            EditorGUILayout.PropertyField(respawnOnDrop);

            EditorGUILayout.Space();
            grab.DisplayActionSettings(
                "Pickup Action",
                "Action to preform when the player grabs this pickup. If User Scaling Limit is set to Restrict"
                + ", then this action is also performed when the player changes avatars while holding this pickup.");

            if (showUseOptions)
            {
                EditorGUILayout.Space();
                useStart.DisplayActionSettings(
                    "Use Start Action",
                    "Action to preform when the player starts using this pickup. If User Scaling Limit is set to Restrict"
                    + ", then this action is also performed when the player changes avatars while using this pickup.");

                EditorGUILayout.Space();
                useEnd.DisplayActionSettings("Use End Action", "Action to preform when the player stops using this pickup.");
            }

            EditorGUILayout.Space();
            drop.DisplayActionSettings("Drop Action", "Action to preform when the player drops this pickup.");

            serializedObject.ApplyModifiedProperties();
        }
    }
}