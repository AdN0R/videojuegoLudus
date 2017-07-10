using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using NodeCanvas.StateMachines;

namespace videojuegoLudus {
    public class WallController : NetworkBehaviour {
        void Start() {
            if (isServer) {
                gameObject.GetComponent<FSMOwner>().enabled = true;
            }
        }
        public void DisableMeshRenderer() {
            RpcDisableMeshRenderer();
        }
        [ClientRpc]
        void RpcDisableMeshRenderer() {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        public void EnableMeshRenderer() {
            RpcEnableMeshRenderer();
        }
        [ClientRpc]
        public void RpcEnableMeshRenderer() {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        public void DisableCollider() {
            RpcDisableCollider();
        }
        [ClientRpc]
        public void RpcDisableCollider() {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        public void EnableColliderr() {
            RpcEnableColliderr();
        }
        [ClientRpc]
        public void RpcEnableColliderr() {
            gameObject.GetComponent<Collider>().enabled = true;
        }
        public bool ServerWall() {
            return isServer;
        }
    }
}
