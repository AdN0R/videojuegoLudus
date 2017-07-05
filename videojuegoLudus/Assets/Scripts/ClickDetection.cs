using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace videojuegoLudus {
    public class ClickDetection : NetworkBehaviour {

        public float Distance = 9999;
        private Animator TrapAnimation;

        private Transform tarjetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;

        // Use this for initialization
        void Awake() {
            
        }

        // Update is called once per frame
        void Update() {
            if(!isLocalPlayer) {
                return;
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
                    TrapAnimation = gameObject.GetComponent<Animator>();
                    //TrapAnimation.
                    Debug.Log("Trap");
                    break;
            }

        }

    }
}
