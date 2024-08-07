using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour {

    public GameObject itemInfoTitle;
    public GameObject itemInfoDesc;
    public GameObject creditsText;

    public void itemPointerExit() {
        itemInfoTitle.GetComponent<TextMeshProUGUI>().text = "";
        itemInfoDesc.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void basicARItemPointerEnter() {
        itemInfoTitle.GetComponent<TextMeshProUGUI>().text = "Basic AR";
        itemInfoDesc.GetComponent<TextMeshProUGUI>().text = @" 
     Damage: 10
     Fire Rate: 900
     Range: 75
     ADS Speed: 0.4
     Magazine: 40
     Reload Time: 0.75";
    }

    public void basicSniperItemPointerEnter() {
        itemInfoTitle.GetComponent<TextMeshProUGUI>().text = "Basic Sniper";
        itemInfoDesc.GetComponent<TextMeshProUGUI>().text = @" 
     Damage: 200
     Fire Rate: 60
     Range: 500
     ADS Speed: 0.8
     Magazine: 8
     Reload Time: 1";
    }

    void Update() {
        creditsText.GetComponent<TextMeshProUGUI>().text = "Credits: " + Player.credits;
    }

}