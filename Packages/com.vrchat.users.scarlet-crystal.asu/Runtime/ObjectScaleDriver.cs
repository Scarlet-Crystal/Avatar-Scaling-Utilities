using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectScaleDriver : UdonSharpBehaviour
    {
        [SerializeField]
        private float baseEyeHeight = 1.5f, maxEyeHeight = 5f, minEyeHeight = 0.2f;

        public float BaseEyeHeight
        {
            get => baseEyeHeight;
            set
            {
                baseEyeHeight = value;
                RecalculateScale(Networking.LocalPlayer);
            }
        }

        public float MaxEyeHeight
        {
            get => maxEyeHeight;
            set
            {
                maxEyeHeight = value;
                RecalculateScale(Networking.LocalPlayer);
            }
        }

        public float MinEyeHeight
        {
            get => minEyeHeight;
            set
            {
                minEyeHeight = value;
                RecalculateScale(Networking.LocalPlayer);
            }
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player.isLocal)
            {
                RecalculateScale(player);
            }
        }

        void OnEnable()
        {
            RecalculateScale(Networking.LocalPlayer);
        }

        private void RecalculateScale(VRCPlayerApi localPlayer)
        {
            if (Utilities.IsValid(localPlayer))
            {
                float scale = Mathf.Clamp(localPlayer.GetAvatarEyeHeightAsMeters(), minEyeHeight, maxEyeHeight) / baseEyeHeight;
                transform.localScale = Vector3.one * scale;
            }
        }
    }
}