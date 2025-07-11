using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI apText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI HPText;

    [Header("Managers")]
    public APManager apManager;
    public ScoreManager scoreManager;
    public GameManager gameManager;
    public HPManager hpManager;

    void Start()
    {
        UpdateAP();
        UpdateScore();
        UpdateStage();
        UpdateHP();
    }

    void Update()
    {
        UpdateAP();
        UpdateScore();
        UpdateStage();
        UpdateHP();
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

    public void UpdateHP()
    {
        HPText.text = "HP: " + hpManager.HP.ToString();
    }
}
