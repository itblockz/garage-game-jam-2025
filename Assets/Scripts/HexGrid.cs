using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour 
{
    [SerializeField] private PrefabPool prefabPool;
    [SerializeField] private float hexSize = 1f;

    private Dictionary<Vector2Int, GameObject> hexes = new Dictionary<Vector2Int, GameObject>();
    
    void Awake() 
    {
        CreateGrid(5, 5);  // Create 5x5 hex grid
    }
    
    void CreateGrid(int width, int height) 
    {
        for (int q = 0; q < width; q++) 
        {
            for (int r = 0; r < height; r++) 
            {
                Vector2Int coord = new Vector2Int(q, r);
                Vector3 worldPos = AxialToWorldPosition(q, r);
                
                GameObject hex = Instantiate(prefabPool.GetRandomPrefab(), worldPos, Quaternion.identity);
                hex.name = $"Hex_{q}_{r}";
                hexes[coord] = hex;
            }
        }
    }
    
    Vector3 AxialToWorldPosition(int q, int r) 
    {
        float x = hexSize * (Mathf.Sqrt(3f)/2f * q + Mathf.Sqrt(3f) * r);
        float y = hexSize * (3f/2f * q);
        return new Vector3(x, y, 0);  // z=0 for flat ground
    }
    
    public List<Vector2Int> GetNeighbors(Vector2Int coord) 
    {
        Vector2Int[] directions = {
            new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, -1),
            new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1)
        };
        
        List<Vector2Int> neighbors = new List<Vector2Int>();
        foreach (var dir in directions) 
        {
            Vector2Int neighbor = coord + dir;
            if (hexes.ContainsKey(neighbor)) 
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    public List<Vector2Int> GetHexCoordinates()
    {
        return new List<Vector2Int>(hexes.Keys);
    }
    
    public GameObject GetHexAt(Vector2Int coord)
    {
        if (hexes.TryGetValue(coord, out GameObject hex))
        {
            return hex;
        }
        return null; // or handle missing hex case
    }

}