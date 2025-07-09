using UnityEngine;

public class TestWallManager : MonoBehaviour
{
    [SerializeField] private WallManager wallManager;

    public void TestAddWall()
    {
        Vector2Int start = new Vector2Int(0, 0);
        Vector2Int end = new Vector2Int(1, 0);
        wallManager.AddWall(start, end);
    }

    public void TestAddWallInvalid()
    {
        Vector2Int start = new Vector2Int(0, 0);
        Vector2Int end = new Vector2Int(2, 0); // Assuming this is not a neighbor
        wallManager.AddWall(start, end);
    }
}
