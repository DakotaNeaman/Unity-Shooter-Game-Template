using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour {

    private TextMeshProUGUI textMesh;

    void Start() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        textMesh.text = Gun.staticCurrentAmmo.ToString() + " / " + Gun.staticMaxAmmo.ToString();
    }
}