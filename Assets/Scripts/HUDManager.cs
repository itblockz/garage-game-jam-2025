using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI apText;
    public TextMeshProUGUI scoreText;

    [Header("Managers")]
    public APManager apManager;
    public ScoreManager scoreManager;

    void Start()
    {
        UpdateAP();
        UpdateScore();
    }

    void Update()
    {
        UpdateAP();
        UpdateScore();
    }

    void UpdateAP()
    {
        apText.text = "Action points: " + apManager.AP.ToString();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + scoreManager.Score.ToString();
    }
}
