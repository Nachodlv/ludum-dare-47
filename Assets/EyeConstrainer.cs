using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EyeConstrainer : MonoBehaviour {
    public float xoffset;
    public float yoffset;

    // Update is called once per frame
    void Update() {
        var position = transform.localPosition;
        transform.localPosition = new Vector3(
            Mathf.Clamp(position.x, -xoffset, xoffset),
            Mathf.Clamp(position.y, -yoffset, yoffset),
            position.z
            );
    }
}
