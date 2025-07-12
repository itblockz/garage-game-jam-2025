using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    private Action action;
    private Image actionImage;

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

    public void SetActionImage(Image newImage)
    {
        if (actionImage != null)
        {
            actionImage.color = Color.white; // Reset previous action image color
        }
        newImage.color = Color.gray;
        actionImage = newImage;
    }
}