using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private APManager apManager;
    [SerializeField] private GridGenerator gridGenerator;
    private int stages = 0;
    private int usedAP = 0;

    public void NextStage()
    {
        stages++;
        if (stages % 5 == 0)
        {
            gridGenerator.IncreaseHexes();
        }
        Debug.Log("Stage " + stages + " completed.");
    }

    public void AddUsedAP()
    {
        usedAP += apManager.GetUsedAP();
        Debug.Log("Total used AP: " + usedAP);
    }

    public void ResetGame()
    {
        stages = 0;
        usedAP = 0;
        Debug.Log("Game reset. Stages and AP usage cleared.");
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
