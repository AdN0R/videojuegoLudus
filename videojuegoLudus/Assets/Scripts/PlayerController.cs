using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Videojuego creado por Andoni y Ruben de prácticas
/// </summary>
namespace videojuegoLudus {
    /// <summary>
    /// Controlador del movimiento del jugador y su interacción con los objetos
    /// </summary>
    public class PlayerController : MonoBehaviour {

        public float speed;
        public int trapDamage;


        Rigidbody rb;
        HealthController hc;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            hc = GetComponent<HealthController>();
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.velocity = movement * speed;

            rb.position = new Vector3(rb.position.x, 0.5f, rb.position.z);

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Trap"))
            {
                hc.TakeDamage(trapDamage);
            }
        }
    }
}
