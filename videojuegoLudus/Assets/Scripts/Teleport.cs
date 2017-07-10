using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    public Vector3 destination = new Vector3(0, 0, 0);
    private GameObject[] players;
    private int i = 0;
	public void TeleportOutside () {
        players = GameObject.FindGameObjectsWithTag("Player");
        while (i < players.Length) {
            players[i].transform.position = destination;
            i++;
        }
	}
}
