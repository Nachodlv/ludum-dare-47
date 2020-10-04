using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Params : MonoBehaviour
    {
        public static Params Instance;

        public bool SlideShowSeen { get; set; }
        public bool IsRetrying { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
