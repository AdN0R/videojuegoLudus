﻿using System.Collections;
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
        public Canvas canvas;

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

            Animating(z);
            Debug.Log("Oli :D");
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
            Debug.Log(Tag);
            switch (Tag) {
                case "Door":
                    StartCoroutine(gameObject.GetComponent<Door>().Open());
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
            //anim.SetTrigger("shot");
            NetworkServer.Spawn(bullet);
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (isLocalPlayer)
            {
                if (other.gameObject.CompareTag("Trap"))
                {
                    hc.TakeDamage(trapDamage);
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
    }
}
