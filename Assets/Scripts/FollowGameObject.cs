using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FollowGameObject : MonoBehaviour
    {
        [SerializeField] private Transform gameObjectToFollow;

        private void Update()
        {
            var myTransform = transform;
            var position = myTransform.position;
            var positionToFollow = gameObjectToFollow.position;
            position.x = positionToFollow.x;
            position.y = positionToFollow.y;
            myTransform.position = position;
        }
    }
}
