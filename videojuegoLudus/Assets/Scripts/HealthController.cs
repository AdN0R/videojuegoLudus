using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Videojuego creado por Andoni y Ruben de prácticas
/// </summary>
namespace videojuegoLudus
{
    /// <summary>
    /// Controlador de la vida y su representación gráfica
    /// </summary>
    public class HealthController : NetworkBehaviour
    {
        public const int startingHealth = 100;
        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth = startingHealth;
        public Slider healthSlider;
        public Image damageImage;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.15f);

        Animator anim;
        bool isDead;
        bool damaged;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
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
        
        [Command]
        void CmdTakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        public void TakeDamage(int amount)
        {
                damaged = true;
                CmdTakeDamage(amount);
        }

        void OnChangeHealth(int health)
        {
            currentHealth = health;
            healthSlider.value = currentHealth;
        }


        void Death()
        {
            isDead = true;
            anim.SetTrigger("die");
        }
    }
}
