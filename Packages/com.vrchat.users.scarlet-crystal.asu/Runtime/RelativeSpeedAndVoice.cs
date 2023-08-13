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
        private AnimationCurve walkCurve, runCurve, strafeCurve, jumpImpulseCurve, gravityCurve;

        [SerializeField]
        private AnimationCurve voiceFarCurve, voiceNearCurve, voiceVolumetricRadiusCurve, voiceGainCurve;

        private void ReconfigureLocomotion(VRCPlayerApi localPlayer)
        {
            float eyeHeight = localPlayer.GetAvatarEyeHeightAsMeters();

            localPlayer.SetWalkSpeed(walkCurve.Evaluate(eyeHeight));
            localPlayer.SetRunSpeed(runCurve.Evaluate(eyeHeight));
            localPlayer.SetStrafeSpeed(strafeCurve.Evaluate(eyeHeight));

            //https://feedback.vrchat.com/vrchat-udon-closed-alpha-bugs/p/1315-small-jump-impulse-and-gravity-strength-values-offsets-the-player-from-the
            localPlayer.SetJumpImpulse (jumpImpulseCurve.Evaluate(eyeHeight));
            localPlayer.SetGravityStrength(gravityCurve.Evaluate(eyeHeight));
        }

        private void ReconfigureVoice(VRCPlayerApi remotePlayer)
        {
            float eyeHeight = remotePlayer.GetAvatarEyeHeightAsMeters();

            remotePlayer.SetVoiceDistanceFar(voiceFarCurve.Evaluate(eyeHeight));
            remotePlayer.SetVoiceDistanceNear(voiceNearCurve.Evaluate(eyeHeight));
            remotePlayer.SetVoiceGain(voiceGainCurve.Evaluate(eyeHeight));

            //Aug 3rd 2023 - Setting remote player's volumetric radius to a small non-zero value prevents VRChat's
            // spacializer from breaking for groups of 20cm players when they are near each other.
            // See https://feedback.vrchat.com/open-beta/p/1314-stereo-separation-for-positional-audio-is-not-corrected-with-scale
            remotePlayer.SetVoiceVolumetricRadius(Mathf.Max(0.005f, voiceVolumetricRadiusCurve.Evaluate(eyeHeight)));
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            if (player.isLocal)
            {
                ReconfigureLocomotion(player);
            }
            else
            {
                ReconfigureVoice(player);
            }
        }
    }
}