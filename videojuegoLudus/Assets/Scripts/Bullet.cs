using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace videojuegoLudus {
    public class Bullet : MonoBehaviour {

        void OnCollisionEnter(Collision collision) {
            var hit = collision.gameObject;
            var health = hit.GetComponent<HealthController>();
            if (health != null) {
                health.TakeDamage(10);
            }

            Destroy(gameObject);
        }
    }
}
