using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTrack : MonoBehaviour {
    public Transform player;
    public CinemachineVirtualCamera camera;

    public float distance = 0;

    public float zoomMultiplier;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update() {
        var position = player.position;
        if (distance == 0) {
            distance = new Vector2(position.x, position.y).magnitude;
        }

        var playerDistance = new Vector2(position.x, position.y).magnitude;
        var halfDistance = (distance +playerDistance*3) / 4;
        transform.position = position.normalized * halfDistance;
        camera.m_Lens.OrthographicSize = zoomMultiplier * 1/halfDistance;
    }
}
