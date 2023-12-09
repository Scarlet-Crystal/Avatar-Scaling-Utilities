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
            StartGuardingScale(Networking.LocalPlayer);
        }
        
        void OnDisable()
        {
            StopGuardingScale(Networking.LocalPlayer);
        }
    }
}