using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

    public float ParalaxMultiplier;

    public GameObject player;

    public float _lastAngle;
    // Start is called before the first frame update

    private void Start() {
        var position = player.transform.position;
        _lastAngle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        var position = player.transform.position;
        var newAngle = MathMod(Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg,360);
        var rotation = _lastAngle - newAngle;
        if (rotation > 300) {
            rotation = 360 - _lastAngle + newAngle;
        }

        if (rotation < -300) {
            rotation = 360 - newAngle + _lastAngle;
        }

        transform.Rotate(0,0, rotation * ParalaxMultiplier);
        _lastAngle = newAngle;
    }
    
    static float MathMod(float a, int b) {
        return a < 0 ? ((a % b) + b) % b : a % b;
    }
}
