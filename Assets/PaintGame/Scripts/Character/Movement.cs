using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
namespace PaintGame.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : NetworkBehaviour
    {
        public Transform handTransform;
        public GroundChecker groundChecker;

        public float speed;
        public float jumpForce;
        public float rotationSpeed;
        public float handSpeed;

        Vector2 move;
        Quaternion look;
        Quaternion hand;
        float jump;

        enum MovementDirtyBits : byte
        {
            None = 0,
            Look = 1,
            Move = 2,
            Jump = 4,
        }
        MovementDirtyBits movementDirtyBits;
        Vector3 inputMove;
        Vector3 inputLook;
        float inputJump;


        new Rigidbody rigidbody;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;

            rotationSpeed *= Time.fixedDeltaTime;
            handSpeed *= Time.fixedDeltaTime;
        }

        public void Move(Vector3 dir)
        {
            inputMove = dir;
            MoveInternal();
        }

        public void Look(Vector3 dir)
        {
            inputLook = dir;
            LookInternal();
        }

        public void Jump(float axis)
        {
            inputJump = axis;
            JumpInternal();
        }

        void FixedUpdate()
        {
            /*if (isServer)
            {
                UpdateClients();
            }
            else if (hasAuthority)
            {
                UpdateServer();
            }*/
            if (!hasAuthority)
                return;

            handTransform.localRotation = Quaternion.RotateTowards(handTransform.localRotation, hand, handSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, rotationSpeed);

            if (groundChecker.IsGrounded())
            {
                var velocity = rigidbody.velocity.SetXZ(move);
                rigidbody.velocity = velocity;

                if (jump == 1)
                {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                }
            }
        }

        #region ServerUpdate
        void UpdateServer()
        {
            /*if (movementDirtyBits != 0)
            {
                CmdTransform(transform.position, transform.rotation);
            }
            if ((movementDirtyBits & MovementDirtyBits.Move) != 0)
            {
                CmdMove(inputMove);
            }
            if ((movementDirtyBits & MovementDirtyBits.Look) != 0)
            {
                CmdLook(inputLook);
            }
            if ((movementDirtyBits & MovementDirtyBits.Jump) != 0)
            {
                CmdJump(inputJump);
            }
            movementDirtyBits = MovementDirtyBits.None;*/
        }

        [Command]
        void CmdTransform(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }
        [Command]
        void CmdMove(Vector3 dir)
        {
            Move(dir);
        }
        [Command]
        void CmdLook(Vector3 dir)
        {
            Look(dir);
        }
        [Command]
        void CmdJump(float jump)
        {
            Jump(jump);
        }

        #endregion

        #region UpdateClients
        void UpdateClients()
        {
            /*if (movementDirtyBits != 0)
            {
                RpcTransform(transform.position, transform.rotation);
            }
            if ((movementDirtyBits & MovementDirtyBits.Move) != 0)
            {
                RpcMove(inputMove);
            }
            if ((movementDirtyBits & MovementDirtyBits.Look) != 0)
            {
                RpcLook(inputLook);
            }
            if ((movementDirtyBits & MovementDirtyBits.Jump) != 0)
            {
                RpcJump(inputJump);
            }
            movementDirtyBits = MovementDirtyBits.None;*/
        }

        [ClientRpc]
        void RpcTransform(Vector3 position, Quaternion rotation)
        {
            if (hasAuthority || isServer)
                return;
            transform.SetPositionAndRotation(position, rotation);
        }

        [ClientRpc]
        void RpcMove(Vector3 dir)
        {
            if (hasAuthority || isServer)
                return;
            inputMove = dir;
            MoveInternal();
        }
        [ClientRpc]
        void RpcLook(Vector3 dir)
        {
            if (hasAuthority || isServer)
                return;
            inputLook = dir;
            LookInternal();
        }
        [ClientRpc]
        void RpcJump(float jump)
        {
            if (hasAuthority || isServer)
                return;
            inputJump = jump;
            JumpInternal();
        }
        #endregion

        #region Internal
        void MoveInternal()
        {
            var dir = inputMove;

            dir.Normalize();

            dir *= speed;

            move.x = dir.x;
            move.y = dir.z;
            movementDirtyBits |= MovementDirtyBits.Move;
        }

        void LookInternal()
        {
            var dir = inputLook;

            dir.Normalize();

            var w2l = transform.worldToLocalMatrix;
            hand = Quaternion.LookRotation(w2l.MultiplyVector(dir).V0YZ());

            look = Quaternion.LookRotation(dir.X0Z());

            movementDirtyBits |= MovementDirtyBits.Look;
        }

        void JumpInternal()
        {
            jump = inputJump;
            movementDirtyBits |= MovementDirtyBits.Jump;
        }
        #endregion
    }
}
