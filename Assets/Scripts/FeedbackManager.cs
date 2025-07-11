using UnityEngine;
using System.Collections.Generic;

public class FeedbackManager : MonoBehaviour
{
    private List<GameObject> activeFeedbacks = new List<GameObject>();

    public void AddFeedback(GameObject feedback)
    {
        if (feedback != null && !activeFeedbacks.Contains(feedback))
        {
            activeFeedbacks.Add(feedback);
            
            // Optional: Start feedback animation if it has SizeFeedback component
            SizeFeedback sizeFeedback = feedback.GetComponent<SizeFeedback>();
            if (sizeFeedback != null)
            {
                sizeFeedback.StartFeedback();
            }
        }
    }

    public void RemoveFeedback(GameObject feedback)
    {
        if (feedback != null && activeFeedbacks.Contains(feedback))
        {
            activeFeedbacks.Remove(feedback);
            
            // Stop feedback animation before deactivating
            SizeFeedback sizeFeedback = feedback.GetComponent<SizeFeedback>();
            if (sizeFeedback != null)
            {
                sizeFeedback.StopFeedback();
            }
        }
    }

    public void ClearAllFeedbacks()
    {
        foreach (var feedback in activeFeedbacks)
        {
            if (feedback != null)
            {
                SizeFeedback sizeFeedback = feedback.GetComponent<SizeFeedback>();
                if (sizeFeedback != null)
                {
                    sizeFeedback.StopFeedback();
                }
            }
        }
        activeFeedbacks.Clear();
    }
}