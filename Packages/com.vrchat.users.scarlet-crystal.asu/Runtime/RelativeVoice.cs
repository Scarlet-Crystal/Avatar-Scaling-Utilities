using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RelativeVoice : UdonSharpBehaviour
    {
        [SerializeField]
        private AnimationCurve voiceFarCurve, voiceNearCurve, voiceVolumetricRadiusCurve, voiceGainCurve;

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
            ReconfigureVoice(player);
        }

        void OnEnable()
        {
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);

            foreach (VRCPlayerApi player in players)
            {
                if (Utilities.IsValid(player))
                {
                    ReconfigureVoice(player);
                }
            }
        }
    }
}