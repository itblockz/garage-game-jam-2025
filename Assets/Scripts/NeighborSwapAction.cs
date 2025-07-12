using System.Collections.Generic;
using UnityEngine;

public class NeighborSwapAction : SwapAction
{
    protected override void OnSelectionComplete(List<GameObject> selectedObjects)
    {
        string selectedObjectsString = string.Join(", ", selectedObjects.ConvertAll(obj => obj.name));
        Debug.Log($"Selected objects: {selectedObjectsString}");
        Vector2Int coord1 = hexGrid.GetCoordinate(selectedObjects[0]);
        Vector2Int coord2 = hexGrid.GetCoordinate(selectedObjects[1]);
        if (hexGrid.IsNeighbor(coord1, coord2) && UseAP())
        {
            swapFeedback.SwapSprites(selectedObjects[0].transform, selectedObjects[1].transform);
            SwapHexes(coord1, coord2);
            AfterExecuteAction();
        }
        else
        {
            Debug.LogWarning($"Selected objects are not neighbors: {coord1} and {coord2}");
            return; // Exit if they are not neighbors
        }
    }

    public override void CancelAction()
    {
        selectionManager.StopSelection();
        Debug.Log("Swap action cancelled.");
    }
}