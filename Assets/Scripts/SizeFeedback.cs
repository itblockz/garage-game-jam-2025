using UnityEngine;

public class SizeFeedback : MonoBehaviour
{
    [Header("Scale Settings")]
    public float minScale = 0.9f;         // ขนาดเล็กสุด
    public float maxScale = 1.1f;         // ขนาดใหญ่สุด
    public float speed = 2.0f;            // ความเร็วในการเต้น

    private float t = 0f;
    private Vector3 originalScale;
    private bool isActive = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isActive)
        {
            t += Time.deltaTime * speed;

            // ใช้ Sine wave เพื่อให้ scale ขึ้นลงแบบ smooth
            float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(t) + 1f) / 2f);
            transform.localScale = originalScale * scale;
        }
    }

    public void StartFeedback()
    {
        isActive = true;
        t = 0f; // Reset animation
    }

    public void StopFeedback()
    {
        isActive = false;
        transform.localScale = originalScale; // Reset to original size
    }
}