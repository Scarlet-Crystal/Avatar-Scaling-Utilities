using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingTrigger : ScalingGuard
    {
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            StartGuardingScale(player);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            StopGuardingScale(player);
        }
    }
}