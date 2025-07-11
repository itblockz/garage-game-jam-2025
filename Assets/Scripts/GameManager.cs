using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private APManager apManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HPManager hpManager;
    [SerializeField] private MoodManager moodManager;
    [SerializeField] private GameObject gameOverPanel;
    private int stages = 0;
    private int usedAP = 0;

    public void NextStage()
    {
        stages++;
        if (stages % 5 == 0)
        {
            gridGenerator.IncreaseHexes();
        }
        gridGenerator.GenerateGrid();
        scoreManager.CalculateScore();
        moodManager.UpdateMood();
        apManager.ResetAP();
        AddUsedAP();
        Debug.Log("Stage " + stages + " completed.");
        hpManager.DecreaseHP(-scoreManager.Score);
        if (hpManager.HP <= 0)
        {
            GameOver();
        }
    }

    public void ResetGame()
    {
        stages = 0;
        usedAP = 0;
        gridGenerator.ResetHexes();
        gridGenerator.GenerateGrid();
        hpManager.ResetHP();
        apManager.ResetAP();
        scoreManager.CalculateScore();
        moodManager.UpdateMood();
        gameOverPanel.SetActive(false);
        Debug.Log("Game reset. Stages: " + stages + ", Used AP: " + usedAP);
    }

    void AddUsedAP()
    {
        usedAP += apManager.GetUsedAP();
        Debug.Log("Total used AP: " + usedAP);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public int Stages
    {
        get { return stages; }
    }

    public int UsedAP
    {
        get { return usedAP; }
    }
}
