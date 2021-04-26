using UnityEngine;

namespace ChatFight
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponData weaponData = null;

        public void Initialize(WeaponData data)
        {
            weaponData = data;
            gameObject.name = data.id;
        }

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
            Debug.LogError($"Collision between {collider.name} and {name}");
            if (collider.tag == Identifiers.tagFighter)
            {
                var fighter = collider.GetComponent<FighterController>();
                DamageFighter(fighter);
            }
        }
    }
}
