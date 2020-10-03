using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graviting : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private float _gravityConstant = 1f; // 9.8f

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assuming that the center of gravity is placed in (0, 0, 0)
        Vector3 vector = gameObject.transform.position.normalized;
        _rigidbody2D.AddForce(vector * -_gravityConstant);
    }
}
