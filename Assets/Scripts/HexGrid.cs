using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private PrefabPool hexPrefabPool;
    [SerializeField] private float hexSize = 1f;

    private Dictionary<Vector2Int, GameObject> hexes = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<GameObject, Vector2Int> hexCoordinates = new Dictionary<GameObject, Vector2Int>();
    private Dictionary<(Vector2Int, Vector2Int), GameObject> walls = new Dictionary<(Vector2Int, Vector2Int), GameObject>();

    public void AddHex(Vector2Int coord)
    {
        if (!hexes.ContainsKey(coord))
        {
            Vector3 worldPos = AxialToWorldPosition(coord.x, coord.y);
            GameObject hex = Instantiate(hexPrefabPool.GetRandomPrefab(), worldPos, Quaternion.identity);
            hex.name = $"Hex_{coord.x}_{coord.y}";
            hexes[coord] = hex;
            hexCoordinates[hex] = coord;
        }
    }

    public void ClearGrid()
    {
        foreach (var hex in hexes.Values)
        {
            Destroy(hex);
        }
        foreach (var wall in walls.Values)
        {
            Destroy(wall);
        }
        hexes.Clear();
        hexCoordinates.Clear();
        walls.Clear();
    }

    Vector3 AxialToWorldPosition(int q, int r)
    {
        float x = hexSize * (Mathf.Sqrt(3f) / 2f * q + Mathf.Sqrt(3f) * r);
        float y = hexSize * (3f / 2f * q);
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

    // get neighbors concern no walls
    public List<Vector2Int> GetNeighborsWithoutWalls(Vector2Int coord)
    {
        List<Vector2Int> neighbors = GetNeighbors(coord);
        List<Vector2Int> validNeighbors = new List<Vector2Int>();

        foreach (var neighbor in neighbors)
        {
            if (!walls.ContainsKey((coord, neighbor)) && !walls.ContainsKey((neighbor, coord)))
            {
                validNeighbors.Add(neighbor);
            }
        }
        return validNeighbors;
    }

    public bool IsNeighbor(Vector2Int coord1, Vector2Int coord2)
    {
        List<Vector2Int> neighbors = GetNeighbors(coord1);
        return neighbors.Contains(coord2);
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

    public void SetHexAt(Vector2Int coord, GameObject hex)
    {
        hexes[coord] = hex;
        hexCoordinates[hex] = coord;
        hex.name = $"Hex_{coord.x}_{coord.y}";
        hex.transform.position = AxialToWorldPosition(coord.x, coord.y);
    }

    public Vector2Int GetCoordinate(GameObject hex)
    {
        if (hexCoordinates.TryGetValue(hex, out Vector2Int coord))
        {
            return coord;
        }
        return Vector2Int.zero; // or handle missing hex case
    }
    public bool AddWall(Vector2Int start, Vector2Int end, GameObject wallPrefab)
    {
        if (IsNeighbor(start, end))
        {
            var wallKey = (start, end);
            if (!walls.ContainsKey(wallKey))
            {
                GameObject wall = Instantiate(wallPrefab, Vector3.Lerp(AxialToWorldPosition(start.x, start.y), AxialToWorldPosition(end.x, end.y), 0.5f), Quaternion.identity);
                Vector3 direction = AxialToWorldPosition(end.x, end.y) - AxialToWorldPosition(start.x, start.y);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                wall.transform.rotation = Quaternion.Euler(0, 0, angle);
                walls[wallKey] = wall;
                wall.name = $"Wall_{start.x}_{start.y}_to_{end.x}_{end.y}";
                return true;
            }
        }
        return false; // Wall already exists or not neighbors
    }
}