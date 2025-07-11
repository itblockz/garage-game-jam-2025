using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class patical_active : MonoBehaviour
{
    public HexData hexData;
    public Sprite angry;
    public Sprite happy;
    public Sprite fire;
    public ParticleSystem patical;

    void Update()
    {
        if (hexData.MoodState == MoodState.Angry)
        {
            patical.textureSheetAnimation.SetSprite(0, angry);
        }
        else
        {
            patical.textureSheetAnimation.SetSprite(0, happy);
        }
    }
}
