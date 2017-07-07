using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace videojuegoLudus {
    public class ChaseController : MonoBehaviour {
        public GameObject[] zombies;

        public void EnableZombies() {
            for (int i = 0; i < zombies.Length; i++) {
                zombies[i].SetActive(true);
            }
        }
    }
}

