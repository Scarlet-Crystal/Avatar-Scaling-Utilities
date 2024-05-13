using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RelativeVoice : UdonSharpBehaviour
    {
        [SerializeField]
        private AnimationCurve voiceFarCurve, voiceNearCurve, voiceVolumetricRadiusCurve, voiceGainCurve;

        public AnimationCurve VoiceFarCurve
        {
            get => voiceFarCurve;
            set => Common.UpdateCurve(value, ref voiceFarCurve);
        }

        public AnimationCurve VoiceNearCurve
        {
            get => voiceNearCurve;
            set => Common.UpdateCurve(value, ref voiceNearCurve);
        }

        public AnimationCurve VoiceVolumetricRadiusCurve
        {
            get => voiceVolumetricRadiusCurve;
            set => Common.UpdateCurve(value, ref voiceVolumetricRadiusCurve);
        }

        public AnimationCurve VoiceGainCurve
        {
            get => voiceGainCurve;
            set => Common.UpdateCurve(value, ref voiceGainCurve);
        }

        private readonly DataDictionary perPlayerOverrides = new DataDictionary();


        private void ReconfigureVoice(VRCPlayerApi remotePlayer)
        {
            AnimationCurve far, near, volumetricRadius, gain;

            if (perPlayerOverrides.TryGetValue(new DataToken(remotePlayer), out var playerOverrides))
            {
                AnimationCurve[] curves = (AnimationCurve[])playerOverrides.Reference;
                far = curves[0] ?? voiceFarCurve;
                near = curves[1] ?? voiceNearCurve;
                volumetricRadius = curves[2] ?? voiceVolumetricRadiusCurve;
                gain = curves[3] ?? voiceGainCurve;
            }
            else
            {
                far = voiceFarCurve;
                near = voiceNearCurve;
                volumetricRadius = voiceVolumetricRadiusCurve;
                gain = voiceGainCurve;
            }

            float eyeHeight = remotePlayer.GetAvatarEyeHeightAsMeters();

            remotePlayer.SetVoiceDistanceFar(far.Evaluate(eyeHeight));
            remotePlayer.SetVoiceDistanceNear(near.Evaluate(eyeHeight));
            remotePlayer.SetVoiceGain(gain.Evaluate(eyeHeight));
            remotePlayer.SetVoiceVolumetricRadius(volumetricRadius.Evaluate(eyeHeight));
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            if (enabled && gameObject.activeInHierarchy)
            {
                ReconfigureVoice(player);
            }
        }

        void OnEnable()
        {
            ReconfigureAllVoices();
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            RemoveOverride(player);
        }

        public void ReconfigureAllVoices()
        {
            VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(players);

            foreach (VRCPlayerApi player in players)
            {
                ReconfigureVoice(player);
            }
        }

        public void SetOverride(
            VRCPlayerApi player,
            AnimationCurve farCurve = null,
            AnimationCurve gainCurve = null,
            AnimationCurve nearCurve = null,
            AnimationCurve volumetricRadiusCurve = null)
        {
            var curves = new AnimationCurve[]
            {
                farCurve,
                nearCurve,
                volumetricRadiusCurve,
                gainCurve
            };

            perPlayerOverrides.SetValue(new DataToken(player), new DataToken(curves));

            ReconfigureVoice(player);
        }

        public void RemoveOverride(VRCPlayerApi player)
        {
            if (Utilities.IsValid(player))
            {
                perPlayerOverrides.Remove(new DataToken(player));
                ReconfigureVoice(player);
            }
        }

#if UNITY_EDITOR && !COMPILER_UDONSHARP

        private void Reset()
        {
            voiceFarCurve = AnimationCurve.Constant(-1, 0, 25f);
            voiceNearCurve = AnimationCurve.Constant(-1, 0, 0f);
            VoiceVolumetricRadiusCurve = AnimationCurve.Constant(-1, 0, 0.005f);
            voiceGainCurve = AnimationCurve.Constant(-1, 0, 15f);
        }

#endif
    }
}