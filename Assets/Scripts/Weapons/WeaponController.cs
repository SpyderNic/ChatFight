using UnityEngine;

namespace ChatFight
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Animator weaponAnimator = null;
        [SerializeField] private WeaponData weaponData = null;
        [SerializeField] private WeaponCollider weaponCollider = null;

        private Transform target = null;
        private float attackTimer = 0.0f;

        public void Awake()
        {
            weaponCollider.OnFighterCollided += DamageFighter;
        }

        public void OnDestroy()
        {
            weaponCollider.OnFighterCollided -= DamageFighter;
        }

        public void Update()
        {
            if (attackTimer > 0.0f)
            {
                attackTimer -= Time.deltaTime;
            }
        }

        private void Attack()
        {
            if (attackTimer <= 0.0f)
            {
                // Attack target
                attackTimer = weaponData.attackCooldown;
                weaponAnimator.SetTrigger(Identifiers.triggerAttack);
            }
        }

        private void CancelAttack()
        {
            // Cancels attack
            weaponAnimator.ResetTrigger(Identifiers.triggerAttack);
        }

        public void DamageFighter(FighterController fighter)
        {
            if (fighter != null)
            {
                // Apply damage to the fighter
                fighter.ApplyDamage(weaponData.damage);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.tag == Identifiers.tagFighter)
            {
                var distance = collider.transform.position - transform.position;
                if (target == null || distance.sqrMagnitude < (target.position - transform.position).sqrMagnitude)
                {
                    // Lock closest target
                    target = collider.transform;
                }

                // Trigger an attack
                Attack();
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == Identifiers.tagFighter)
            {
                if (target == collider.transform)
                {
                    // The target is out of range or destroyed
                    CancelAttack();
                    target = null;
                }
            }
        }
    }
}
