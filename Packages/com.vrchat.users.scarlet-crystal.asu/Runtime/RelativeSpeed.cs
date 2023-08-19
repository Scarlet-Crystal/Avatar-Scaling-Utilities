using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RelativeSpeed : UdonSharpBehaviour
    {
        [SerializeField]
        private AnimationCurve walkCurve, runCurve, strafeCurve, jumpImpulseCurve, gravityCurve;

        private void ReconfigureLocomotion(VRCPlayerApi localPlayer)
        {
            float eyeHeight = localPlayer.GetAvatarEyeHeightAsMeters();

            localPlayer.SetWalkSpeed(walkCurve.Evaluate(eyeHeight));
            localPlayer.SetRunSpeed(runCurve.Evaluate(eyeHeight));
            localPlayer.SetStrafeSpeed(strafeCurve.Evaluate(eyeHeight));

            //https://feedback.vrchat.com/vrchat-udon-closed-alpha-bugs/p/1315-small-jump-impulse-and-gravity-strength-values-offsets-the-player-from-the
            localPlayer.SetJumpImpulse(jumpImpulseCurve.Evaluate(eyeHeight));
            localPlayer.SetGravityStrength(gravityCurve.Evaluate(eyeHeight));
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi localPlayer, float oldEyeHeight)
        {
            if (localPlayer.isLocal)
            {
                ReconfigureLocomotion(localPlayer);
            }
        }

        void OnEnable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Utilities.IsValid(localPlayer))
            {
                ReconfigureLocomotion(localPlayer);
            }
        }
    }
}