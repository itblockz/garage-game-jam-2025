using UnityEngine;

public class APManager : MonoBehaviour
{
    [SerializeField] private int initialAP = 21;
    private int ap;

    private void Start()
    {
        ResetAP(); // Initialize AP at the start
    }

    public void ResetAP()
    {
        AP = initialAP; // Reset AP to the initial value
    }

    public int AP
    {
        get { return ap; }
        set
        {
            ap = value;
            if (ap < 0)
            {
                ap = 0; // Ensure AP does not go below 0
            }
        }
    }

    public bool HasEnoughAP(int cost)
    {
        return AP >= cost;
    }

    public void UseAP(int cost)
    {
        if (HasEnoughAP(cost))
        {
            AP -= cost;
        }
        else
        {
            Debug.LogWarning("Not enough AP to perform this action.");
        }
    }
    
    public int GetUsedAP()
    {
        return initialAP - AP; // Calculate used AP
    }
}