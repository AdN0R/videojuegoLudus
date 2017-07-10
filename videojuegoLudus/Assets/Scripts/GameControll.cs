using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace videojuegoLudus {
    public class GameControll : MonoBehaviour {
        public Text winText;
        public Color textColor = new Color(1f, 1f, 1f, 1f);

        public void winGame() {
            winText.color = textColor;
            Time.timeScale = 0;
        }
    }
}
