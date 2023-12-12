using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace AvatarScalingUtilities.ExampleScene
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RespawningPickup : UdonSharpBehaviour
    {
        [SerializeField]
        private bool respawnOnDrop, respawnOnUseDown, respawnOnUseUp;

        private Vector3 startPos;
        private Quaternion startRot;
        private VRC_Pickup vrcPickup;
        private VRCObjectSync vrcObjectSync;
        private new Rigidbody rigidbody;

        void Start()
        {
            vrcPickup = GetComponent<VRC_Pickup>();
            vrcObjectSync = GetComponent<VRCObjectSync>();
            rigidbody = GetComponent<Rigidbody>();
            startPos = transform.position;
            startRot = transform.rotation;
        }

        public override void OnPickupUseDown()
        {
            if (respawnOnUseDown)
            {
                RespawnPickup();
            }
        }

        public override void OnPickupUseUp()
        {
            if (respawnOnUseUp)
            {
                RespawnPickup();
            }
        }

        public override void OnDrop()
        {
            if (respawnOnDrop)
            {
                RespawnPickup();
            }
        }

        private void RespawnPickup()
        {
            vrcPickup.Drop();

            if (vrcObjectSync != null)
            {
                if (Networking.IsOwner(gameObject))
                {
                    vrcObjectSync.Respawn();
                }
            }
            else
            {
                rigidbody.position = startPos;
                rigidbody.rotation = startRot;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}