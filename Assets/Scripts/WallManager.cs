using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private GameObject wallPrefab;

    public void AddWall(Vector2Int start, Vector2Int end)
    {
        if (hexGrid.AddWall(start, end, wallPrefab))
        {
            Debug.Log($"Wall added between {start} and {end}");
        }
        else
        {
            Debug.LogWarning($"Failed to add wall between {start} and {end}");
        }
    }
}
