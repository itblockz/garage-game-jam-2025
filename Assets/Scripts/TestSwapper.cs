using UnityEngine;

public class TestSwapper : MonoBehaviour
{
    [SerializeField] private Swapper swapper;

    public void TestSwap()
    {

        // Example coordinates to swap
        Vector2Int coord1 = new Vector2Int(0, 0);
        Vector2Int coord2 = new Vector2Int(1, 0);

        swapper.SwapHexes(coord1, coord2);
    }
}
