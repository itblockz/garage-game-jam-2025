using UnityEngine;

public class ActionController : MonoBehaviour
{
    private Action action;

    public void PerformAction()
    {
        action.PerformAction();
    }

    public void CancelAction()
    {
        if (action != null)
        {
            action.CancelAction();
            Debug.Log("Action cancelled: " + action.GetType().Name);
        }
        else
        {
            Debug.LogWarning("No action to cancel.");
        }
    }

    public void SetAction(Action newAction)
    {
        if (action != null && action != newAction)
        {
            action.CancelAction();
        }
        action = newAction;
        Debug.Log("Action set to: " + action.GetType().Name);
    }
}