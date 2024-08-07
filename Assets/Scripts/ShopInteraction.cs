using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopInteraction : MonoBehaviour {

    public LayerMask playerMask;
    public GameObject player;
    public GameObject interactInfoText;
    public GameObject shop;
    public GameObject hud;

    public static bool shopOpen = false;

    public float radius = 3f;
    private bool playerInBounds;

    private TextMeshProUGUI textMesh;
    public string infoText = "Open shop"; //Open shop (e) --- ???

    void Start() {
        textMesh = interactInfoText.GetComponent<TextMeshProUGUI>();
        shop.SetActive(false);
    }

    void Update() {

        playerInBounds = Physics.CheckSphere(transform.position, radius, playerMask);
            if(playerInBounds) {
                    //Informs the player that interaction is possible
                    textMesh.text = infoText;
                if(Input.GetButtonDown("Interact")) {
                    if(shopOpen) {
                        //Close GUI
                        Pause.paused = false;
                        shopOpen = false;
                        shop.SetActive(false);
                        hud.SetActive(true);
                    } else {
                        //Open GUI
                            if(Pause.paused == false) {
                        Pause.paused = true;
                        shopOpen = true;
                        shop.SetActive(true);
                        hud.SetActive(false);
                            }
                    }
                }
                    if(Input.GetButtonDown("Exit") && shopOpen) {
                        //Close GUI
                        shopOpen = false;
                        shop.SetActive(false);
                        hud.SetActive(true);
                    }
        } else {
            textMesh.text = "";
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}