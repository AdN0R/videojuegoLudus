using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace videojuegoLudus {
    public class CustomNetworkManager : NetworkManager {

        public GameObject Supervisor;
        public GameObject Player;

        public void OnClientConnect(Networking.NetworkConnection connection) {
            //Se decide que Prefab es usado dependiendo de si es el host o el cliente
            if (connection.isClient) {

            }
        }
    }
}
