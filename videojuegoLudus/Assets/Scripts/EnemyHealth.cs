using UnityEngine;
using UnityEngine.Networking;

namespace videojuegoLudus {
    public class EnemyHealth : NetworkBehaviour {
        public int startingHealth = 100;
        public int currentHealth;
        public AudioClip deathClip;


        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        bool isDead;

        void Awake() {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            currentHealth = startingHealth;
        }


        public void TakeDamage(int amount, Vector3 hitPoint) {
            if (isDead)
                return;

            enemyAudio.Play();

            currentHealth -= amount;

            hitParticles.transform.position = hitPoint;
            hitParticles.Play();

            if (currentHealth <= 0) {
                Death();
            }
        }


        void Death() {
            isDead = true;

            capsuleCollider.isTrigger = true;

            enemyAudio.clip = deathClip;
            enemyAudio.Play();
            RcpDestroyEnemy(gameObject.name);
        }
        void RcpDestroyEnemy(string name) {
            Destroy(GameObject.Find(name));
        }
    }
}
