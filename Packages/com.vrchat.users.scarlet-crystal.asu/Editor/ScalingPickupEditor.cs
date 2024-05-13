using UnityEditor;

namespace AvatarScalingUtilities
{
    [CustomEditor(typeof(ScalingPickup))]
    [CanEditMultipleObjects]
    class ScalingPickupEditor : Editor
    {
        private ActionSettings grab, useStart, useEnd, drop;

        void OnEnable()
        {
            grab = new ActionSettings(serializedObject, "grab");
            useStart = new ActionSettings(serializedObject, "useStart");
            useEnd = new ActionSettings(serializedObject, "useEnd");
            drop = new ActionSettings(serializedObject, "drop");
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpEditor.UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            serializedObject.Update();

            EditorGUILayout.Space();
            grab.DisplayActionSettings(
                "Pickup Action",
                "Action to preform when the player grabs this pickup. If User Scaling Limit is set to Restrict"
                + ", then this action is also performed when the player changes avatars while holding this pickup.");

            EditorGUILayout.Space();
            drop.DisplayActionSettings("Drop Action", "Action to preform when the player drops this pickup.");

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(
                "The use actions have no effect if autohold is disabled on the attached VRC Pickup component.",
                MessageType.Info);

            EditorGUILayout.Space();
            useStart.DisplayActionSettings(
                "Use Start Action",
                "Action to preform when the player starts using this pickup. If User Scaling Limit is set to Restrict"
                + ", then this action is also performed when the player changes avatars while using this pickup.");

            EditorGUILayout.Space();
            useEnd.DisplayActionSettings("Use End Action", "Action to preform when the player stops using this pickup.");

            serializedObject.ApplyModifiedProperties();
        }
    }
}