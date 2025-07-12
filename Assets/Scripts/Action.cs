using System;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [SerializeField] private APManager apManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private MoodManager moodManager;
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
    public abstract void CancelAction();

    protected bool UseAP()
    {
        if (apManager.HasEnoughAP(apCost))
        {
            apManager.UseAP(apCost);
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough AP to use this action.");
            return false;
        }
    }

    protected void AfterExecuteAction()
    {
        scoreManager.CalculateScore();
        moodManager.UpdateMood();
    }

    internal void Invoke()
    {
        throw new NotImplementedException();
    }
}