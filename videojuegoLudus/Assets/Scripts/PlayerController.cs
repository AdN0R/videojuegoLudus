using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

/// <summary>
///  Videojuego creado por Andoni y Ruben de prácticas
/// </summary>
namespace videojuegoLudus {
    /// <summary>
    /// Controlador del movimiento del jugador y su interacción con los objetos
    /// </summary>
    public class PlayerController : NetworkBehaviour {

        public float speed;
        public int trapDamage;
        public float Distance = 9999;
        public Camera cam;
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        private Transform tarjetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;

        Rigidbody rb;
        HealthController hc;
    
        private void Start()
        {              
            rb = GetComponent<Rigidbody>();
            hc = GetComponent<HealthController>();
            if (!isLocalPlayer) {
                cam.enabled = false;
            }
            
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer) {
                return;
            }
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetKeyDown(KeyCode.Space)) {
                CmdFire();
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetButtonDown("Fire1")) {
                if (Physics.Raycast(ray, out hit, Distance)) {
                    ClickObject(hit.collider.gameObject);


                }
            }
        }
        void ClickObject(GameObject gameObject) { //Funcion para activar la acción de cada objeto
            string Tag = gameObject.tag;
            Debug.Log(Tag);
            switch (Tag) {
                case "Door":
                    gameObject.transform.Rotate(0, 90, 0);
                    break;
                case "Trap":
                    //TrapAnimation = gameObject.GetComponent<Animator>();
                    //TrapAnimation.
                    Debug.Log("Trap");
                    break;
            }
        }
        [Command]
        void CmdFire() {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
            NetworkServer.Spawn(bullet);
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }
        void OnCollisionEnter(Collision other) {
            if (other.gameObject.tag == "Player") {
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }

            void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Trap"))
            {
                hc.TakeDamage(trapDamage);
            }
        }

        public override void OnStartLocalPlayer() {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
