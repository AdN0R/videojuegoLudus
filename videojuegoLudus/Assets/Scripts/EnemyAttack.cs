﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace videojuegoLudus {
    public class EnemyAttack : NetworkBehaviour {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;


        Animator anim;
        GameObject player;
        HealthController playerHealth;
        EnemyHealth enemyHealth;
        bool playerInRange;
        float timer;


        void Awake() {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<HealthController>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();
        }


        void OnTriggerEnter(Collider other) {
            if (other.gameObject == player) {
                playerInRange = true;
            }
        }


        void OnTriggerExit(Collider other) {
            if (other.gameObject == player) {
                playerInRange = false;
            }
        }


        void Update() {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
                Attack();
            }
        }


        void Attack() {
            timer = 0f;

            if (playerHealth.currentHealth > 0) {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
