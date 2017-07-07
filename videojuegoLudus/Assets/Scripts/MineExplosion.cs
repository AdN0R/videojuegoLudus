using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
namespace videojuegoLudus {
    public class MineExplosion : NetworkBehaviour {
        public GameObject explosion1;
        // Use this for initialization
        void Start() {
            explosion1.SetActive(true);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}

