using System;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [SerializeField] private APManager apManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int apCost = 0;

    public void PerformAction()
    {
        if (apManager.HasEnoughAP(apCost))
        {
            ExecuteAction();
        }
        else
        {
            Debug.LogWarning("Not enough AP to perform this action.");
        }
    }

    protected abstract void ExecuteAction();

    protected void UseAP()
    {
        apManager.UseAP(apCost);
        Debug.Log($"Action performed. AP cost: {apCost}. Remaining AP: {apManager.AP}");
    }

    protected void CalculateScore()
    {
        scoreManager.CalculateScore();
        Debug.Log($"Score after action: {scoreManager.Score}");
    }

    internal void Invoke()
    {
        throw new NotImplementedException();
    }
}