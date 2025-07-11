using System;
using System.Collections.Generic;
using UnityEngine;

public class SwapAction : Action
{
    [SerializeField] protected HexGrid hexGrid;
    [SerializeField] protected SelectionManager selectionManager;
    [SerializeField] protected SwapFeedback swapFeedback;

    protected override void ExecuteAction()
    {
        selectionManager.StartSelection(2, OnSelectionComplete);
    }

    protected void SwapHexes(Vector2Int coord1, Vector2Int coord2)
    {
        GameObject hex1 = hexGrid.GetHexAt(coord1);
        GameObject hex2 = hexGrid.GetHexAt(coord2);

        if (hex1 != null && hex2 != null)
        {
            hexGrid.SetHexAt(coord1, hex2);
            hexGrid.SetHexAt(coord2, hex1);
        }
    }

    protected virtual void OnSelectionComplete(List<GameObject> selectedObjects)
    {
        String selectedObjectsString = string.Join(", ", selectedObjects.ConvertAll(obj => obj.name));
        Debug.Log($"Selected objects: {selectedObjectsString}");
        Vector2Int coord1 = hexGrid.GetCoordinate(selectedObjects[0]);
        Vector2Int coord2 = hexGrid.GetCoordinate(selectedObjects[1]);
        swapFeedback.SwapSprites(selectedObjects[0].transform, selectedObjects[1].transform);
        SwapHexes(coord1, coord2);
    }
}