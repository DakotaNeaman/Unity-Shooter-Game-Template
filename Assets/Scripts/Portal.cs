using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform player;
    public float xPos;
    public float yPos;
    public float zPos;

    public Transform playerCheck;
    public float playerDistance = 0.4f;
    public LayerMask playerMask;

    public string destinationName;
    private bool touchingPlayer;

    void Update() {
        
        touchingPlayer = Physics.CheckSphere(playerCheck.position, playerDistance, playerMask);

        if(touchingPlayer) {
            player.position = new Vector3(xPos, yPos, zPos);
            Player.playerLocation = destinationName;
        }

    }

}