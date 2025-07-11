using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;

    private void Start()
    {
        UpdateMood();
        Debug.Log("Initial Mood Updated");
    }

    public void UpdateMood()
    {
        List<Vector2Int> hexes = hexGrid.GetHexCoordinates();
        foreach (Vector2Int coord in hexes)
        {
            GameObject hex = hexGrid.GetHexAt(coord);
            List<Vector2Int> neighbors = hexGrid.GetNeighborsWithoutWalls(coord);
            if (hex != null)
            {
                HexData hexData = hex.GetComponent<HexData>();
                if (hexData != null)
                {
                    hexData.MoodState = MoodState.Neutral;
                    foreach (Vector2Int neighborCoord in neighbors)
                    {
                        GameObject neighborHex = hexGrid.GetHexAt(neighborCoord);
                        if (neighborHex != null)
                        {
                            HexData neighborData = neighborHex.GetComponent<HexData>();
                            if (neighborData != null && hexData.EnemiesHexTypes.Contains(neighborData.HexType))
                            {
                                hexData.MoodState = MoodState.Angry;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}