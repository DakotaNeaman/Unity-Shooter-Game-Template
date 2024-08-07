using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {

    public float rotationSpeed;

    void Start() {
        
    }

    void Update() {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
