using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AvatarScalingUtilities
{
    [RequireComponent(typeof(ParticleSystem))]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScalingParticles : UdonSharpBehaviour
    {
        [SerializeField]
        private ScalingActions collisionAction;

        [SerializeField]
        private ScalingLimits collisionLimit;

        [SerializeField]
        private float collisionA, collisionB;

        [SerializeField]
        private AnimationCurve collisionCurve;


        public override void OnPlayerParticleCollision(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                Common.ExecuteAction(player, collisionAction, collisionLimit, collisionA, collisionB, collisionCurve);
            }
        }

#if UNITY_EDITOR && !COMPILER_UDONSHARP

        private void Reset()
        {
            collisionCurve = Common.CreateDefaultRemapCurve();
        }

#endif
    }
}