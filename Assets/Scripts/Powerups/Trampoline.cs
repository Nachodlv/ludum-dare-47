using Sound;
using UnityEngine;

namespace DefaultNamespace.Powerups
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float constantPower;
        [SerializeField] private SoundWithSettings sound;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            var racer = other.gameObject.GetComponent<Racer>();
            if (rigidbody2D == null || racer == null) return;

            rigidbody2D.AddForce(-rigidbody2D.position.normalized *
                                 (constantPower + rigidbody2D.velocity.magnitude * power));
            Debug.Log(constantPower + rigidbody2D.velocity.magnitude * power);
            AudioManager.instance.PlaySound(sound, transform.position);
        }
    }
}
