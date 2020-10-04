using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "New Sound With Settings", menuName = "Sound/Sound With Settings", order = 0)]
    public class SoundWithSettings : ScriptableObject
    {
        public AudioClip audioClip;
        public float volume = 1;
    }
}
