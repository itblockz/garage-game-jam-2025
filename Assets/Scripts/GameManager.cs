using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private APManager apManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HPManager hpManager;
    [SerializeField] private MoodManager moodManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private ActionController actionController;
    private int stages = 0;
    private int usedAP = 0;
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    public void NextStage()
    {
        hpManager.DecreaseHP(-scoreManager.Score);
        if (scoreManager.Score < 0)
        {
            audioSource.PlayOneShot(loseClip);
        }
        if (scoreManager.Score == 0) {
            audioSource.PlayOneShot(winClip);
        }
        if (hpManager.HP <= 0)
        {
            GameOver();
            return;
        }
        AddUsedAP();
        stages++;
        if (stages % 5 == 0)
        {
            gridGenerator.IncreaseHexes();
        }
        gridGenerator.GenerateGrid();
        scoreManager.CalculateScore();
        moodManager.UpdateMood();
        apManager.ResetAP();
        actionController.CancelAction();
        Debug.Log("Stage " + stages + " completed.");
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

    public void SetHighestStage()
    {
        int highStage = GetHighestStage();
        int bestAP = GetBestAP();
        if (stages > highStage || (stages == highStage && usedAP < bestAP))
        {
            PlayerPrefs.SetInt("HighestStage", stages);
            PlayerPrefs.SetInt("BestAP", usedAP);
            PlayerPrefs.Save();
            Debug.Log("New high score set: Stages - " + stages + ", Used AP - " + usedAP);
        }
    }

    public int GetHighestStage()
    {
        return PlayerPrefs.GetInt("HighestStage", 0);
    }
    
    public int GetBestAP()
    {
        return PlayerPrefs.GetInt("BestAP", 0);
    }
    
    public bool IsNewHighScore()
    {
        int highStage = GetHighestStage();
        int bestAP = GetBestAP();
        return stages > highStage || (stages == highStage && usedAP < bestAP);
    }

    void AddUsedAP()
    {
        usedAP += apManager.GetUsedAP();
        Debug.Log("Total used AP: " + usedAP);
    }

    void GameOver()
    {
        SetHighestStage();
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
