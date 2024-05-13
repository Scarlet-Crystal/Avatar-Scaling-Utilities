using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace AvatarScalingUtilities
{
    [RequireComponent(typeof(VRC.SDK3.Components.VRCStation))]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingStation : ScalingGuard
    {
        public override void OnStationEntered(VRCPlayerApi player)
        {
            StartGuardingScale(player);
        }

        public override void OnStationExited(VRCPlayerApi player)
        {
            StopGuardingScale(player);
        }
    }
}