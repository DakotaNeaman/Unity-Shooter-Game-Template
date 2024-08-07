using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyNameSetting : MonoBehaviour {

    private TextMeshProUGUI textMesh;
    private GameObject greatGrandParent;
    new public string name = "Name";

    void Start() {
        greatGrandParent = transform.parent.parent.parent.gameObject;
        name = greatGrandParent.GetComponent<Enemy>().enemyName;
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        textMesh.text = name;
    }
}