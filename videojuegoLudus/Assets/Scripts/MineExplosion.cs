using System.Collections;
using NodeCanvas.StateMachines;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
namespace videojuegoLudus {
    public class MineExplosion : NetworkBehaviour {
        public GameObject explosion1;
        public bool FSM = false;
        // Use this for initialization
        void Start() {
            explosion1.SetActive(true);
            ActivateMine();

        }

        void ActivateMine() {
            gameObject.GetComponent<SphereCollider>().radius = 0.9f;
            Invoke("ColliderDisable", 1);
            if (FSM){
                gameObject.GetComponent<FSMOwner>().enabled = false;
            }

        }
        void ColliderDisable() {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            CmdDeleteMine(gameObject.name, 3);
        }
        [Command]
        void CmdDeleteMine(string name, int time) {
            RpcDeleteMine(name, time);
        }
        [ClientRpc]
        void RpcDeleteMine(string name, int time) {
            Destroy(GameObject.Find(name), time);
        }
        void OnCollisionEnter(Collision collision) {
            var hit = collision.gameObject;
            var health = hit.GetComponent<HealthController>();
            var healthEnemy = hit.GetComponent<EnemyHealth>();

            
            if (collision.gameObject.tag == "mine") {
                //Explotar otras minas
            }
            if (health != null) {
                health.TakeDamage(30);
            }
            if (healthEnemy != null) {
                healthEnemy.TakeDamage(100, collision.transform.position);
            }
        }
    }
}

