using NodeCanvas.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
namespace videojuegoLudus {
    public class MineMovement : NetworkBehaviour {
        private bool FSM;
        // Use this for initialization
        void Start() {
            FSM = gameObject.GetComponent<MineExplosion>().FSM;
            if (isServer && FSM) {
                gameObject.GetComponent<FSMOwner>().enabled = true;
            }
        }
    }
}
