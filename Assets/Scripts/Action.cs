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
            apManager.UseAP(apCost);
            ExecuteAction();
        }
        else
        {
            Debug.LogWarning("Not enough AP to perform this action.");
        }
    }

    protected abstract void ExecuteAction();

    internal void Invoke()
    {
        throw new NotImplementedException();
    }
}