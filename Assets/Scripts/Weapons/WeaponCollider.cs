using System;
using UnityEngine;

namespace ChatFight
{
    public class WeaponCollider : MonoBehaviour
    {
        public event Action<FighterController> OnFighterCollided;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == Identifiers.tagFighter)
            {
                var fighter = collider.GetComponent<FighterController>();
                OnFighterCollided?.Invoke(fighter);
            }
        }
    }
}
