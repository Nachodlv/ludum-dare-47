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
        var newAngle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
        transform.Rotate(0,0, (_lastAngle - newAngle) * ParalaxMultiplier);
        _lastAngle = newAngle;
    }
}
