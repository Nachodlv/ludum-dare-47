using UnityEngine;
using Utils;

namespace DefaultNamespace.Terrain
{
    public class Clouds : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        private void Update()
        {
            var myTransform = transform;
            myTransform.RotateAround(Vector3.zero, Vector3.forward, Time.deltaTime * movementSpeed);
            Debug.Log(Time.deltaTime * movementSpeed);
        }
    }
}
