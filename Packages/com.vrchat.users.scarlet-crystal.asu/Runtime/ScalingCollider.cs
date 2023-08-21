using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingCollider : ScalingGuard
    {
        public override void OnPlayerCollisionEnter(VRCPlayerApi player)
        {
            StartGuardingScale(player);
        }

        public override void OnPlayerCollisionExit(VRCPlayerApi player)
        {
            StopGuardingScale(player);
        }
    }
}