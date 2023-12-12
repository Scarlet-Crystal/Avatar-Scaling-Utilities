using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class ScalingGuard : UdonSharpBehaviour
    {
        [SerializeField]
        private ScalingActions startAction;

        [SerializeField]
        private ScalingLimits startLimit;

        [SerializeField]
        private float startA, startB;

        [SerializeField]
        private AnimationCurve startCurve;


        [SerializeField]
        private ScalingActions stopAction;

        [SerializeField]
        private ScalingLimits stopLimit;

        [SerializeField]
        private float stopA, stopB;

        [SerializeField]
        private AnimationCurve stopCurve;


        private bool isGuarding = false;
        private bool avatarChanged = false;

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                avatarChanged = true;
            }
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            if (player.isLocal)
            {
                if (isGuarding)
                {
                    Common.HandleEyeHeightChanged(player, avatarChanged, startAction, startLimit, startA, startB, startCurve);
                }

                avatarChanged = false;
            }
        }

        protected void StartGuardingScale(VRCPlayerApi localPlayer)
        {
            if (!isGuarding && localPlayer.isLocal)
            {
                isGuarding = true;
                Common.ExecuteAction(localPlayer, startAction, startLimit, startA, startB, startCurve);
            }
        }

        protected void StopGuardingScale(VRCPlayerApi localPlayer)
        {
            if (isGuarding && localPlayer.isLocal)
            {
                isGuarding = false;
                Common.ExecuteAction(localPlayer, stopAction, stopLimit, stopA, stopB, stopCurve);
            }
        }

    }
}