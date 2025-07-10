using UnityEngine;

public class APManager : MonoBehaviour
{
    [SerializeField] private int ap = 15;

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
}