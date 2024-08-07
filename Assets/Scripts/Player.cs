using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static float maxPlayerHealth = 100f;
    public static float playerHealth = 100f;
    public static float credits = 0f;

    public float minimumY = -200f;
    public float interactDistance = 10f;

    public GameObject player;
    public GameObject damageAlert;

    public static string playerLocation;

    void Start() {
        playerLocation = "Lobby";
        damageAlert.SetActive(false);
    }

    void Die() {
        player.transform.position = new Vector3(0, 0, 0);
        playerLocation = "Lobby";
        playerHealth = maxPlayerHealth;
        damageAlert.SetActive(false);
    }

    void Update() {
        Debug.Log(playerLocation);
        if(player.transform.position.y < minimumY || playerHealth <= 0f) {
            Die();
        }
    }
}