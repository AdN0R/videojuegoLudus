using NodeCanvas.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MineMovement : NetworkBehaviour {

    // Use this for initialization
    void Start() {
        if (isServer) {
            gameObject.GetComponent<FSMOwner>().enabled = true;
        }
    }
}
