using UnityEngine;

namespace ChatFight
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData = null;

        private void Attack()
        {
            // Acquire target

            // Attack target
        }

        public void DamageFighter(FighterController fighter)
        {
            if (fighter != null)
            {
                // Apply damage to the fighter
                fighter.ApplyDamage(weaponData.damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == Identifiers.tagFighter)
            {
                var fighter = collider.GetComponent<FighterController>();
                DamageFighter(fighter);
            }
        }
    }
}
