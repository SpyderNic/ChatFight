using System.Collections;
using TMPro;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public TMP_Text fighterName = null;
    public SpriteRenderer spriteRenderer = null;
    public Transform canvasTransform = null;
    public Rigidbody2D rigidBody = null;

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
        canvasTransform.SetParent(null);

        // Start jumping
        StartCoroutine(Jump());
    }

    private void LateUpdate()
    {
        // Update name canvas position each frame
        canvasTransform.position = transform.position + Vector3.up * 0.8f;
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
}
