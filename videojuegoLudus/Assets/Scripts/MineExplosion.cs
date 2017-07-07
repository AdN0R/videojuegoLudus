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

        void OnCollisionEnter(Collision collision) {
            var hit = collision.gameObject;
            var health = hit.GetComponent<HealthController>();
            var healthEnemy = hit.GetComponent<EnemyHealth>();

            gameObject.GetComponent<SphereCollider>().radius = 0.9f;
            if (collision.gameObject.tag == "mine") {
                //Explotar otras minas
            }
            Invoke("ColliderDisable",1);


            if (health != null) {
                health.TakeDamage(30);
            }
            if (healthEnemy != null) {
                healthEnemy.TakeDamage(100, collision.transform.position);
            }
        }
        void ColliderDisable() {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 3);
        }
    }
}

