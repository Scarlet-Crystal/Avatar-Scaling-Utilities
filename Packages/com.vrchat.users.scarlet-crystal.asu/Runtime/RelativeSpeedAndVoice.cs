using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RelativeSpeedAndVoice : UdonSharpBehaviour
    {
        [SerializeField]
        private AnimationCurve walkCurve, runCurve, strafeCurve, jumpImpulseCurve, gravityCurve, voiceRangeCurve;

        private void ReconfigureLocomotion(float height, VRCPlayerApi localPlayer)
        {
            localPlayer.SetWalkSpeed(walkCurve.Evaluate(height));
            localPlayer.SetRunSpeed(runCurve.Evaluate(height));
            localPlayer.SetStrafeSpeed(strafeCurve.Evaluate(height));

            //https://feedback.vrchat.com/vrchat-udon-closed-alpha-bugs/p/1315-small-jump-impulse-and-gravity-strength-values-offsets-the-player-from-the
            localPlayer.SetJumpImpulse (jumpImpulseCurve.Evaluate(height));
            localPlayer.SetGravityStrength(gravityCurve.Evaluate(height));
        }

        private void ReconfigureVoice(float height, VRCPlayerApi remotePlayer)
        {
            remotePlayer.SetVoiceDistanceFar(voiceRangeCurve.Evaluate(height));

            //Aug 3rd 2023 - Setting remote player's volumetric radius to a small non-zero value prevents VRChat's
            // spacializer from breaking for groups of 20cm players when they are near each other.
            // See https://feedback.vrchat.com/open-beta/p/1314-stereo-separation-for-positional-audio-is-not-corrected-with-scale
            remotePlayer.SetVoiceVolumetricRadius(0.005f);
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            float newEyeHeight = player.GetAvatarEyeHeightAsMeters();

            if (player.isLocal)
            {
                ReconfigureLocomotion(newEyeHeight, player);
            }
            else
            {
                ReconfigureVoice(newEyeHeight, player);
            }
        }
    }
}