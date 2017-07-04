using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace videojuegoLudus {
    public class ClickDetection : MonoBehaviour {

        public string Tag = "Clickable";
        public float Distance = 9999;

        private Transform tarjetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;

        // Use this for initialization
        void Awake() {

        }

        // Update is called once per frame
        void Update() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetButtonDown("Fire1")) {
                if (Physics.Raycast(ray, out hit, Distance)) {
                    ClickObject(hit.collider.gameObject);


                }
            }

        }
        void ClickObject(GameObject gameObject) { //Funcion
            if (gameObject.CompareTag(Tag)) {
                // Avisar por consola
                Debug.Log("Objeto clickable");
            }
        }

    }
}
