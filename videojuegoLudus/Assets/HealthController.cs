using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Videojuego creado por Andoni y Ruben de prácticas
/// </summary>
namespace videojuegoLudus{
    /// <summary>
    /// Controlador de la vida y su representación gráfica
    /// </summary>
    public class HealthController : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.15f);

        PlayerController playerController;
        bool isDead;
        bool damaged;
        
        void Awake()
        {
            playerController = GetComponent<PlayerController>();
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
            damaged = true;
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }


        void Death()
        {
            isDead = true;
            playerController.enabled = false;
        }
    }
}
