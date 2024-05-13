using VRC.SDKBase;

namespace AvatarScalingUtilities
{
    public class ScalingObject : ScalingGuard
    {
        void OnEnable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;

            if (Utilities.IsValid(localPlayer))
            {
                StartGuardingScale(localPlayer);
            }
        }

        void OnDisable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;

            if (Utilities.IsValid(localPlayer))
            {
                StopGuardingScale(localPlayer);
            }
        }
    }
}