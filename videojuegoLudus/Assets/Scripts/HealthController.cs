using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Videojuego creado por Andoni y Ruben de prácticas
/// </summary>
namespace videojuegoLudus{
    /// <summary>
    /// Controlador de la vida y su representación gráfica
    /// </summary>
    public class HealthController : NetworkBehaviour
    {
        public int startingHealth = 100;
        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.15f);


        Rigidbody rb;
        bool isDead;
        bool damaged;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Awake()
        {
            currentHealth = startingHealth;
        }


        void Update()
        {
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }


        public void TakeDamage(int amount)
        {
            if (!isServer) {
                return;
            }
            damaged = true;
            currentHealth -= amount;
            
            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }
        void OnChangeHealth(int health) {
            healthSlider.value = currentHealth;
        }


        void Death()
        {
            isDead = true;
            rb.isKinematic = true;
        }
    }
}
