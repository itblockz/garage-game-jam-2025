using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Swapper : MonoBehaviour
{
    [SerializeField] protected HexGrid hexGrid;
    [SerializeField] private ScoreManager scoreManager;

    public virtual void SwapHexes(Vector2Int coord1, Vector2Int coord2)
    {
        GameObject hex1 = hexGrid.GetHexAt(coord1);
        GameObject hex2 = hexGrid.GetHexAt(coord2);

        if (hex1 != null && hex2 != null)
        {
            hexGrid.SetHexAt(coord1, hex2);
            hexGrid.SetHexAt(coord2, hex1);
        }
        scoreManager.CalculateScore();
        Debug.Log($"Swapped hexes at {coord1} and {coord2}. New score: {scoreManager.Score}");
    }
}
