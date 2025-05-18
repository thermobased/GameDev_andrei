using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.0f;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float fadeSpeed = 1.0f;
    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            Debug.LogError("TextMeshPro component not found on DamageText object");
        }
    }

    public void Setup(float damageAmount, Color textColor)
    {
        textMesh.text = damageAmount.ToString("0");
        textMesh.color = textColor;
        StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        float elapsed = 0f;
        Color startColor = textMesh.color;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * moveSpeed;

        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / lifetime;

            // Move upward
            transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);

            // Fade out
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(1f, 0f, normalizedTime * fadeSpeed);
            textMesh.color = currentColor;

            yield return null;
        }

        Destroy(gameObject);
    }
}