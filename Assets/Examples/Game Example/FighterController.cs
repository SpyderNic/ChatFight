using System.Collections;
using TMPro;
using UnityEngine;

namespace ChatFight
{
    public class FighterController : MonoBehaviour
    {
        public const int MaxHealth = 100;

        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private Rigidbody2D rigidBody = null;
        [SerializeField] private Transform canvasTransform = null;
        [SerializeField] private TMP_Text fighterName = null;
        [SerializeField] private ProgressBar healthBar = null;

        private int currentHealth = MaxHealth;
        private Vector3 originalOffset = Vector3.zero;

        public void Start()
        {
            // Start on full heath
            ApplyHeal(MaxHealth);
            originalOffset = canvasTransform.position - transform.position;
        }

        public void Initialize(Chatter chatter)
        {
            // Set chatter's name
            //
            // If chatter's display name is "font safe" then use it. Otherwise use login name.
            // Login name is always lowercase and can only contain characters: a-z, A-Z, 0-9, _
            //
            fighterName.text = chatter.IsDisplayNameFontSafe() ? chatter.tags.displayName : chatter.login;
            fighterName.color = ColorUtility.TryParseHtmlString(chatter.tags.colorHex, out var colour) ? colour : Color.white;

            // Change box color
            //
            if (chatter.HasBadge("moderator"))
                spriteRenderer.color = Color.green; // Green box if chatter has moderator badge
            else
            if (chatter.HasBadge("vip"))
                spriteRenderer.color = Color.magenta; // Magenta box if chatter has VIP badge
            else
            if (chatter.HasBadge("subscriber"))
                spriteRenderer.color = Color.red; // Red box if chatter has subscriber badge

            // Detach name canvas from parent so that it doesn't rotate
            //canvasTransform.SetParent(null);

            // Start jumping
            StartCoroutine(Jump());
        }

        public void ApplyDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
            UpdateHealthBar();
        }

        public void ApplyHeal(int heal)
        {
            currentHealth += heal;
            if (currentHealth >= MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBar.TweenToProgress((float)currentHealth / MaxHealth, 0.5f);
            healthBar.Text.SetText($"{currentHealth} / {MaxHealth}");
        }

        private void LateUpdate()
        {
            // Update name canvas position each frame
            canvasTransform.rotation = Quaternion.identity;
            canvasTransform.position = transform.position + originalOffset;
        }

        private IEnumerator Jump()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));

                float dir = Random.value > 0.5f ? 1.0f : -1.0f; // Random jump direction
                rigidBody.AddForce(Vector2.up * 10.0f + (Vector2.right * 3.0f) * dir, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            ApplyDamage(10);
        }
    }
}
