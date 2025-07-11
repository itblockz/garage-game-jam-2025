﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapFeedback : MonoBehaviour
{
    public float moveSpeed = 2.0f; // ความเร็วในการเคลื่อนที่
    private Transform spriteB; // ตำแหน่งของ Sprite B
    private Transform spriteA; // ตำแหน่งของ Sprite A

    private bool isSwapping = false;

    public void SwapSprites(Transform spriteA, Transform spriteB)
    {
        this.spriteA = spriteA;
        this.spriteB = spriteB;

        // เริ่ม Coroutine สำหรับการสลับ Sprite
        StartCoroutine(SwapSprites());
    }

    IEnumerator SwapSprites()
    {
        if (isSwapping)
        {
            yield break; // ถ้าอยู่ในระหว่างการสลับแล้ว ให้หยุดการทำงาน
        }

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
