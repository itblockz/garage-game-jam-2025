using System;
using System.Collections.Generic;
using UnityEngine;

public class WallCreateAction : Action
{
    [SerializeField] private HexGrid hexGrid;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private GameObject wallPrefab;

    protected override void ExecuteAction()
    {
        selectionManager.StartSelection(2, OnSelectionComplete);
    }

    void AddWall(Vector2Int start, Vector2Int end)
    {
        if (hexGrid.AddWall(start, end, wallPrefab))
        {
            Debug.Log($"Wall added between {start} and {end}");
        }
        else
        {
            Debug.LogWarning($"Failed to add wall between {start} and {end}");
        }
    }

    protected virtual void OnSelectionComplete(List<GameObject> selectedObjects)
    {
        String selectedObjectsString = string.Join(", ", selectedObjects.ConvertAll(obj => obj.name));
        Debug.Log($"Selected objects: {selectedObjectsString}");
        Vector2Int coord1 = hexGrid.GetCoordinate(selectedObjects[0]);
        Vector2Int coord2 = hexGrid.GetCoordinate(selectedObjects[1]);
        if (hexGrid.IsNeighbor(coord1, coord2))
        {
            AddWall(coord1, coord2);
        }
        else
        {
            Debug.LogWarning($"Selected objects {selectedObjects[0].name} and {selectedObjects[1].name} are not neighbors.");
        }
    }
}