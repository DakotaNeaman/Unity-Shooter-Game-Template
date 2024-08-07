using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSetting : MonoBehaviour {

    private TextMeshProUGUI textMesh;
    new public string name = "Name"; 

    void Start() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        textMesh.text = name;
    }
}