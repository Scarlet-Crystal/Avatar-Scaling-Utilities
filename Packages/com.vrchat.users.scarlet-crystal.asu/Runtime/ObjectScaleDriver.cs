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
        private float scaleMultiplier = 1f;

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player.isLocal)
            {
                transform.localScale = Vector3.one * (player.GetAvatarEyeHeightAsMeters() * scaleMultiplier);
            }
        }
    }
}