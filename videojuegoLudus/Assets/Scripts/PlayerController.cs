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
        private NetworkStartPosition[] spawnPoints;
        public float speed;
        public int trapDamage;
        public float Distance = 9999;
        public Camera cam;
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public Canvas canvas;
        public float bulletSpeed;

        private Transform tarjetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;

        HealthController hc;
        Animator anim;
    
        private void Start()
        {              
            hc = GetComponent<HealthController>();
            if (!isLocalPlayer) {
                cam.enabled = false;
                canvas.transform.GetChild(0).position = new Vector3(150, 0, 0);
            }
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
            if (Input.GetKey(KeyCode.LeftShift) && isServer){
                z *= 3;
            }
            Animating(z);
            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);

            if (Input.GetKeyDown(KeyCode.Space) && isServer)
            {
                anim.SetTrigger("shot");
                CmdFire();
            }
            if (!isServer)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Input.GetButtonDown("Fire1") && (Physics.Raycast(ray, out hit, Distance)))
                {
                    ClickObject(hit.collider.gameObject);
                }
            }

        }

        void ClickObject(GameObject gameObject) { //Funcion para activar la acción de cada objeto
            string Tag = gameObject.tag;
            switch (Tag) {
                case "Door":
                    CmdOpenDoor(gameObject.name);
                    break;
                case "Mine":
                    CmdExplosiveTrap(gameObject.name);
                    break;
                case "Trap":

                    break;
            }
        }
        [Command]
        void CmdOpenDoor(string name) {
            RpcOpenDoor(name);
        }
        [ClientRpc]
        void RpcOpenDoor(string name) {
            StartCoroutine(GameObject.Find(name).GetComponent<Door>().Open());
        }
        [Command]
        void CmdExplosiveTrap(string name) {
            RpcExplosiveTrap(name);
        }
        [ClientRpc]
        void RpcExplosiveTrap(string name) {
            GameObject.Find(name).GetComponent<MineExplosion>().enabled = true;
        }

        [Command]
        void CmdFire() {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            //anim.SetTrigger("shot");
            RpcSetTrigger("shot");
            NetworkServer.Spawn(bullet);
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }
        [ClientRpc]
        public void RpcSetTrigger(string trigger) {
            anim.SetTrigger(trigger);
        }

        void OnTriggerEnter(Collider other)
        {
            if (isLocalPlayer)
            {
                if (other.gameObject.CompareTag("Trap"))
                {
                    hc.TakeDamage(trapDamage);
                }
                if (other.gameObject.CompareTag("Mine")) {
                    CmdExplosiveTrap(other.name);
                }
            }
        }

        public override void OnStartLocalPlayer()
        {
            // GameObject soldier = player.transform.GetChild(1).renderer
            //player.transform.GetComponentInChildren<Renderer>().material.color = Color.green;
        }

        void Animating(float z)
        {
            bool walking = z != 0f;
            anim.SetBool("isMoving", walking);
        }
        [ClientRpc]
        void RpcRespawn() {
            if (isLocalPlayer) {
                // Set the spawn point to origin as a default value
                Vector3 spawnPoint = Vector3.zero;

                // If there is a spawn point array and the array is not empty, pick one at random
                if (spawnPoints != null && spawnPoints.Length > 0) {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                }

                // Set the player’s position to the chosen spawn point
                transform.position = spawnPoint;
            }
        }
        public void Death() {
            RpcRespawn();
        }
    }
}
