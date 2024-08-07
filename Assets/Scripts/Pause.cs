using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    
public GameObject player;
public static bool paused = false;

void Update() {
    if(paused) {
        player.GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0f;
    } else {
        player.GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1f;
    }

    if(Input.GetButtonDown("Pause")) {
        if(paused) {
            paused = false;
        } else {
            paused = true;
        }
    }
}

}
