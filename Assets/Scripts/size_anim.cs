using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class size_anim : MonoBehaviour
{
    public float minScale = 0.9f;         // ขนาดเล็กสุด
    public float maxScale = 1.1f;         // ขนาดใหญ่สุด
    public float speed = 2.0f;            // ความเร็วในการเต้น

    private Vector3 originalScale;
    private float t = 0f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        // ใช้ Sine wave เพื่อให้ scale ขึ้นลงแบบ smooth
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(t) + 1f) / 2f);
        transform.localScale = originalScale * scale;
    }
}