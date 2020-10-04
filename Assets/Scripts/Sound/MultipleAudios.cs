using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "New Multiple Audios", menuName = "Sound/Multiple Audio", order = 0)]
    public class MultipleAudios : ScriptableObject
    {
        public List<SoundWithSettings> sounds;
        public SoundWithSettings RandomSound => sounds[Random.Range(0, sounds.Count)];
    }
}
