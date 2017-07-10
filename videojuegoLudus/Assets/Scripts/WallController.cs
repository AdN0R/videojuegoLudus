using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace videojuegoLudus {
    public class WallController : MonoBehaviour {

        public void DisableMeshRenderer() {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        public void EnableMeshRenderer() {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        public void DisableCollider() {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        public void EnableColliderr() {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
