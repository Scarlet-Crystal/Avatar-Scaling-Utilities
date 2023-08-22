using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Components;

namespace AvatarScalingUtilities
{
    [RequireComponent(typeof(VRCPickup))]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingPickup : UdonSharpBehaviour
    {
        [SerializeField]
        private ScalingActions grabAction;

        [SerializeField]
        private ScalingLimits grabLimit;

        [SerializeField]
        private float grabA, grabB;

        [SerializeField]
        private AnimationCurve grabCurve;


        [SerializeField]
        private ScalingActions useStartAction;

        [SerializeField]
        private ScalingLimits useStartLimit;

        [SerializeField]
        private float useStartA, useStartB;

        [SerializeField]
        private AnimationCurve useStartCurve;


        [SerializeField]
        private ScalingActions useEndAction;

        [SerializeField]
        private ScalingLimits useEndLimit;

        [SerializeField]
        private float useEndA, useEndB;

        [SerializeField]
        private AnimationCurve useEndCurve;


        [SerializeField]
        private ScalingActions dropAction;

        [SerializeField]
        private ScalingLimits dropLimit;

        [SerializeField]
        private float dropA, dropB;

        [SerializeField]
        private AnimationCurve dropCurve;


        private bool avatarChanged = false;
        private PickupState pickupState;

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                avatarChanged = true;
            }
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (player.isLocal)
            {
                switch (pickupState)
                {
                    case PickupState.Released:
                        break;

                    case PickupState.Held:
                        Common.HandleEyeHeightChanged(player, avatarChanged, grabAction, grabLimit, grabA, grabB, grabCurve);
                        break;

                    case PickupState.Using:
                        Common.HandleEyeHeightChanged(player, avatarChanged, useStartAction, useStartLimit, useStartA, useStartB, useStartCurve);
                        break;
                }

                avatarChanged = false;
            }
        }

        public override void OnPickup()
        {
            pickupState = PickupState.Held;
            Common.ExecuteAction(Networking.LocalPlayer, grabAction, grabLimit, grabA, grabB, grabCurve);
        }

        public override void OnPickupUseDown()
        {
            pickupState = PickupState.Using;
            Common.ExecuteAction(Networking.LocalPlayer, useStartAction, useStartLimit, useStartA, useStartB, useStartCurve);
        }

        public override void OnPickupUseUp()
        {
            pickupState = PickupState.Held;
            Common.ExecuteAction(Networking.LocalPlayer, useEndAction, useEndLimit, useEndA, useEndB, useEndCurve);
        }

        public override void OnDrop()
        {
            pickupState = PickupState.Released;
            Common.ExecuteAction(Networking.LocalPlayer, dropAction, dropLimit, dropA, dropB, dropCurve);
        }
    }

    enum PickupState
    {
        Released,
        Held,
        Using
    }
}