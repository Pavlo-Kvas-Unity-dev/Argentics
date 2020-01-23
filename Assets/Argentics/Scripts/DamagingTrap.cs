using Unity.Collections;
using UnityEngine;

namespace Argentics
{
    public class DamagingTrap : MonoBehaviour
    {
        [SerializeField] private float timeBetweenDamages = 1f;

        private float prevHurtTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;

            ApplyDamage(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag != "Player") return;

            //check if player should be hurt again
            if (Time.time - prevHurtTime < timeBetweenDamages) return;

            ApplyDamage(other);
        }

        private void ApplyDamage(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable.Damage();

            //record the damage time
            prevHurtTime = Time.time;
        }
    }
}
