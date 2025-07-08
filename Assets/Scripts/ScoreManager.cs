using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGrid;
    private int score = 0;

    public int Score
    {
        get => score;
        private set => score = value;
    }

    void Start()
    {
        CalculateScore();
        Debug.Log("Initial Score: " + score);
    }

    public void CalculateScore()
    {
        int score = 0;
        List<Vector2Int> hexes = hexGrid.GetHexCoordinates();
        foreach (Vector2Int coord in hexes)
        {
            GameObject hex = hexGrid.GetHexAt(coord);
            List<Vector2Int> neighbors = hexGrid.GetNeighbors(coord);
            if (hex != null)
            {
                HexData hexData = hex.GetComponent<HexData>();
                if (hexData != null)
                {
                    foreach (Vector2Int neighborCoord in neighbors)
                    {
                        GameObject neighborHex = hexGrid.GetHexAt(neighborCoord);
                        if (neighborHex != null)
                        {
                            HexData neighborData = neighborHex.GetComponent<HexData>();
                            if (neighborData != null && hexData.EnemiesHexTypes.Contains(neighborData.HexType))
                            {
                                // If the neighbor is an enemy, decrease score
                                score--;
                            }
                        }
                    }
                }
            }
        }
        this.score = score;
    }
}
