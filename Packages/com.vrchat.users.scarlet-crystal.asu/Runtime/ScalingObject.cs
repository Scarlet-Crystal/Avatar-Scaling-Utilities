using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    public class ScalingObject : ScalingGuard
    {
        void OnEnable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;

            if (Utilities.IsValid(localPlayer))
            {
                StartGuardingScale(Networking.LocalPlayer);
            }
        }

        void OnDisable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            
            if (Utilities.IsValid(localPlayer))
            {
                StopGuardingScale(Networking.LocalPlayer);
            }
        }
    }
}