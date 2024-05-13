using UdonSharp;
using VRC.SDKBase;

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