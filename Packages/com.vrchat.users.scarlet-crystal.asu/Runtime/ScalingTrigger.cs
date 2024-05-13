using UdonSharp;
using VRC.SDKBase;

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