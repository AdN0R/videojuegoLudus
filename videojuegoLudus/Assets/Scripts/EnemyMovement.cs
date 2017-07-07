using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace videojuegoLudus {
    public class EnemyMovement : NetworkBehaviour {
        Transform player;
        HealthController playerHealth;
        EnemyHealth enemyHealth;
        UnityEngine.AI.NavMeshAgent nav;


        void Awake() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<HealthController>();
            enemyHealth = GetComponent<EnemyHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }


        void Update() {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
                nav.SetDestination(player.position);
            } else {
                nav.enabled = false;
            }
        }
    }
}
   
