using UnityEngine;

public class NeighborSwapper : Swapper
{
    public override void SwapHexes(Vector2Int coord1, Vector2Int coord2)
    {
        if (hexGrid.IsNeighbor(coord1, coord2))
        {
            base.SwapHexes(coord1, coord2);
        }
        else
        {
            Debug.LogWarning($"Cannot swap non-neighbor hexes at {coord1} and {coord2}");
        }
    }
}
