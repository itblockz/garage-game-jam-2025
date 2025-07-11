using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private int minHexes = 5;
    [SerializeField] private int maxHexes = 12;
    private int hexNumber;

    void Awake()
    {
        ResetHexes();
        GenerateGrid();
    }

    public void IncreaseHexes()
    {
        hexNumber = Mathf.Min(maxHexes, hexNumber + 1);
    }

    public void ResetHexes()
    {
        hexNumber = minHexes;
    }

    public void GenerateGrid()
    {
        hexGrid.ClearGrid();
        Vector2Int[] directions = {
            new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, -1),
            new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1)
        };
        List<Vector2Int> hexes = new List<Vector2Int>();
        List<Vector2Int> hexesToAdd = new List<Vector2Int>
        {
            new Vector2Int(gridWidth / 2, gridHeight / 2) // Start with the center hex
        };
        while (hexes.Count < hexNumber && hexesToAdd.Count > 0)
        {
            int index = Random.Range(0, hexesToAdd.Count);
            Vector2Int currentHex = hexesToAdd[index];
            hexes.Add(currentHex);
            hexGrid.AddHex(currentHex);
            hexesToAdd.RemoveAt(index);
            foreach (var direction in directions)
            {
                Vector2Int neighborHex = currentHex + direction;
                if (!hexes.Contains(neighborHex) && !hexesToAdd.Contains(neighborHex) &&
                    IsWithinBounds(neighborHex, gridWidth, gridHeight))
                {
                    hexesToAdd.Add(neighborHex);
                }
            }
        }
    }

    bool IsWithinBounds(Vector2Int hex, int width, int height)
    {
        return hex.x >= 0 && hex.x < width && hex.y >= 0 && hex.y < height;
    }
}
