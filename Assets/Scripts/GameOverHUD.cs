using TMPro;
using UnityEngine;

public class GameOverHUD : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI apText;

    [Header("Game Manager Reference")]
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        stageText.text = "Total Stages: " + gameManager.Stages;
        apText.text = "Total Used AP: " + gameManager.UsedAP;
    }
}