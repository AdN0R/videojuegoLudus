using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using NodeCanvas.BehaviourTrees;

namespace videojuegoLudus {
    public class EnemyMovement : NetworkBehaviour {
        Transform player;
        HealthController playerHealth;
        EnemyHealth enemyHealth;
        UnityEngine.AI.NavMeshAgent nav;


        void OnEnable() {
            /*if (isServer) {
                gameObject.GetComponent<BehaviourTreeOwner>().enabled = true;
            }*/
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Mine")) {
                other.GetComponent<MineExplosion>().enabled = true;
            }
        }
    }
}
   
