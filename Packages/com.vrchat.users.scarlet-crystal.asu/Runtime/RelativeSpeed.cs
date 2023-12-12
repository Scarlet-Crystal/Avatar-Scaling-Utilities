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

        public AnimationCurve WalkCurve
        {
            get => walkCurve;
            set => Common.UpdateCurve(value, ref walkCurve);
        }

        public AnimationCurve RunCurve
        {
            get => runCurve;
            set => Common.UpdateCurve(value, ref runCurve);
        }

        public AnimationCurve StrafeCurve
        {
            get => strafeCurve;
            set => Common.UpdateCurve(value, ref strafeCurve);
        }

        public AnimationCurve JumpImpulseCurve
        {
            get => jumpImpulseCurve;
            set => Common.UpdateCurve(value, ref jumpImpulseCurve);
        }

        public AnimationCurve GravityCurve
        {
            get => gravityCurve;
            set => Common.UpdateCurve(value, ref gravityCurve);
        }

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

        public void ReconfigureLocomotion()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Utilities.IsValid(localPlayer) && enabled && gameObject.activeInHierarchy)
            {
                ReconfigureLocomotion(localPlayer);
            }
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            if (player.isLocal && enabled && gameObject.activeInHierarchy)
            {
                ReconfigureLocomotion(player);
            }
        }

        void OnEnable()
        {
            ReconfigureLocomotion();
        }
    }
}