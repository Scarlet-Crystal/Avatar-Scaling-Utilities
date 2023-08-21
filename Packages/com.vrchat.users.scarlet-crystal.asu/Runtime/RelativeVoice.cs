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
            remotePlayer.SetVoiceVolumetricRadius(voiceVolumetricRadiusCurve.Evaluate(eyeHeight));
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