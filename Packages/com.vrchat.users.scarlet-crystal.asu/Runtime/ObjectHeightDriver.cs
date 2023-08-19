using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectHeightDriver : UdonSharpBehaviour
    {
        [SerializeField]
        private float minEyeHeight, maxEyeHeight;

        public float MaxEyeHeight
        {
            get => maxEyeHeight;
            set
            {
                maxEyeHeight = value;
                RecalculateHeight(Networking.LocalPlayer);
            }
        }

        public float MinEyeHeight
        {
            get => minEyeHeight;
            set
            {
                minEyeHeight = value;
                RecalculateHeight(Networking.LocalPlayer);
            }
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player.isLocal)
            {
                RecalculateHeight(player);
            }
        }

        void OnEnable()
        {
            RecalculateHeight(Networking.LocalPlayer);
        }

        private void RecalculateHeight(VRCPlayerApi localPlayer)
        {
            if (Utilities.IsValid(localPlayer))
            {
                float height = Mathf.Clamp(localPlayer.GetAvatarEyeHeightAsMeters(), minEyeHeight, maxEyeHeight);
                transform.localPosition = new Vector3(0, height, 0);
            }
        }
    }
}