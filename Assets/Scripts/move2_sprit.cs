using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2_sprit : MonoBehaviour
{
    public Transform spriteA; // ตำแหน่งของ Sprite A
    public Transform spriteB; // ตำแหน่งของ Sprite B
    public float moveSpeed = 2.0f; // ความเร็วในการเคลื่อนที่

    private bool isSwapping = false;

    void Update()
    {
        // กดปุ่ม Space เพื่อเริ่มการสลับ
        if (Input.GetKeyDown(KeyCode.F) && !isSwapping)
        {
            StartCoroutine(SwapSprites());
        }
    }

    IEnumerator SwapSprites()
    {
        isSwapping = true;

        Vector3 posA = spriteA.position;
        Vector3 posB = spriteB.position;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * moveSpeed;

            // เคลื่อน spriteA ไปหา posB และ spriteB ไปหา posA แบบ smooth
            spriteA.position = Vector3.Lerp(posA, posB, progress);
            spriteB.position = Vector3.Lerp(posB, posA, progress);

            yield return null;
        }

        // ให้แน่ใจว่า sprite ถึงตำแหน่งเป้าหมายพอดี
        spriteA.position = posB;
        spriteB.position = posA;

        isSwapping = false;
    }
}
