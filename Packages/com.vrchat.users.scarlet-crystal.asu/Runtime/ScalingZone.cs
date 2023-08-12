using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingZone : UdonSharpBehaviour
    {
        [SerializeField]
        private ScalingActions insideAction;

        [SerializeField]
        private ScalingLimits insideLimit;

        [SerializeField]
        private float insideA, insideB;

        [SerializeField]
        private AnimationCurve insideCurve;


        [SerializeField]
        private ScalingActions leaveAction;

        [SerializeField]
        private ScalingLimits leaveLimit;

        [SerializeField]
        private float leaveA, leaveB;

        [SerializeField]
        private AnimationCurve leaveCurve;


        private bool isInsideTrigger = false;
        private bool avatarChanged = false;

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            avatarChanged = true;
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float oldEyeHeight)
        {
            if (isInsideTrigger)
            {
                Common.HandleEyeHeightChanged(player, avatarChanged, insideAction, insideLimit, insideA, insideB, insideCurve);
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            isInsideTrigger = true;
            Common.ExecuteAction(player, insideAction, insideLimit, insideA, insideB, insideCurve);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            isInsideTrigger = false;
            Common.ExecuteAction(player, leaveAction, leaveLimit, leaveA, leaveB, leaveCurve);
        }
    }
}