using TMPro;
using UnityEngine;

public class GameStartHUD : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI apText;

    [Header("Game Manager Reference")]
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        stageText.text = "Total Stages: " + gameManager.GetHighestStage();
        apText.text = "Total Used AP: " + gameManager.GetBestAP();
        Debug.Log("HUD Updated: Stages - " + gameManager.GetHighestStage() + ", Used AP - " + gameManager.GetBestAP());
    }
}