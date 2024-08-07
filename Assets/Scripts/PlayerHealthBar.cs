using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {

    public Slider slider;

    void Start() {
        slider.maxValue = Player.playerHealth;
        slider.value = Player.playerHealth;
    }

    void Update() {
        slider.value = Player.playerHealth;
    }

}