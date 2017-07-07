using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace videojuegoLudus {
    public class Bullet : MonoBehaviour {

        void OnCollisionEnter(Collision collision) {
            var hit = collision.gameObject;
            var health = hit.GetComponent<HealthController>();
            var healthEnemy = hit.GetComponent<EnemyHealth>();
            if (health != null) {
                health.TakeDamage(10);
            }
            if(healthEnemy != null) {
                healthEnemy.TakeDamage(20, collision.transform.position);
            }

            Destroy(gameObject);
        }
    }
}
