using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI apText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stageText;

    [Header("Managers")]
    public APManager apManager;
    public ScoreManager scoreManager;
    public GameManager gameManager;

    void Start()
    {
        UpdateAP();
        UpdateScore();
        UpdateStage();
    }

    void Update()
    {
        UpdateAP();
        UpdateScore();
        UpdateStage();
    }

    void UpdateAP()
    {
        apText.text = "Action points: " + apManager.AP.ToString();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + scoreManager.Score.ToString();
    }

    public void UpdateStage()
    {
        stageText.text = "Stage " + (gameManager.Stages + 1).ToString();
    }
}
