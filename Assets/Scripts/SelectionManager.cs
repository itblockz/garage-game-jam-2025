using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class SelectionManager : MonoBehaviour
{
    private List<GameObject> selectedObjects = new List<GameObject>();
    private int requiredSelectionCount = 2; // How many objects need to be selected to complete
    
    [Header("Selection Settings")]
    public LayerMask selectableLayer = -1;

    [Header("Feedback")]
    public FeedbackManager feedbackManager;

    private UnityEvent<List<GameObject>> OnSelectionComplete;
    private UnityEvent OnSelectionCancelled;
    private UnityEvent<GameObject> OnObjectSelected;
    private UnityEvent<GameObject> OnObjectDeselected;

    private Camera mainCamera;
    private bool isSelectionActive = false;
    private Action<List<GameObject>> selectionCompleteCallback;
    private Action selectionCancelledCallback;
    
    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            mainCamera = FindObjectOfType<Camera>();
    }
    
    void Update()
    {
        if (isSelectionActive)
        {
            HandleMouseInput();
        }
    }
    
    void HandleMouseInput()
    {
        // Left click to select
        if (Input.GetMouseButtonDown(0))
        {
            SelectObjectAtMousePosition();
        }
        
        // Right click to cancel selection
        if (Input.GetMouseButtonDown(1))
        {
            CancelSelection();
        }

        // ESC key to cancel selection
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelSelection();
        }
    }
    
    void SelectObjectAtMousePosition()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, selectableLayer);
        
        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            
            if (selectedObjects.Contains(clickedObject))
            {
                DeselectObject(clickedObject);
            }
            else
            {
                SelectObject(clickedObject);
            }
        }
    }

    void SelectObject(GameObject obj)
    {
        selectedObjects.Add(obj);
        Debug.Log($"Selected: {obj.name} ({selectedObjects.Count}/{requiredSelectionCount})");
        ApplySelectionFeedback(obj);
        
        // Trigger object selected event
        OnObjectSelected?.Invoke(obj);
        
        // Check if selection is complete
        if (selectedObjects.Count >= requiredSelectionCount)
        {
            CompleteSelection();
        }
    }
    
    void DeselectObject(GameObject obj)
    {
        if (selectedObjects.Contains(obj))
        {
            selectedObjects.Remove(obj);
            RemoveSelectionFeedback(obj);
            
            // Trigger object deselected event
            OnObjectDeselected?.Invoke(obj);
            
            Debug.Log($"Deselected: {obj.name} ({selectedObjects.Count}/{requiredSelectionCount})");
        }
    }
    
    void DeselectAll()
    {
        foreach (GameObject obj in selectedObjects)
        {
            RemoveSelectionFeedback(obj);
            OnObjectDeselected?.Invoke(obj);
        }
        selectedObjects.Clear();
        
        Debug.Log("Deselected all objects");
    }

    void ApplySelectionFeedback(GameObject obj)
    {
        if (feedbackManager != null)
        {
            feedbackManager.AddFeedback(obj);
        }
    }
    
    void RemoveSelectionFeedback(GameObject obj)
    {
        if (feedbackManager != null)
        {
            feedbackManager.RemoveFeedback(obj);
        }
    }

    void CompleteSelection()
    {
        isSelectionActive = false;
        List<GameObject> completedSelection = new List<GameObject>(selectedObjects);
        
        Debug.Log($"Selection completed with {completedSelection.Count} objects");
        
        // Clear current selection
        DeselectAll();
        
        // Trigger events
        OnSelectionComplete?.Invoke(completedSelection);
        selectionCompleteCallback?.Invoke(completedSelection);
        
        // Clear callbacks
        selectionCompleteCallback = null;
        selectionCancelledCallback = null;
    }

    void CancelSelection()
    {
        isSelectionActive = false;
        
        Debug.Log("Selection cancelled");
        
        // Clear current selection
        DeselectAll();
        
        // Trigger events
        OnSelectionCancelled?.Invoke();
        selectionCancelledCallback?.Invoke();
        
        
        // Clear callbacks
        selectionCompleteCallback = null;
        selectionCancelledCallback = null;
    }

    // PUBLIC METHODS FOR EXTERNAL CALLING

    public void StartSelection(int required, Action<List<GameObject>> onComplete = null, Action onCancel = null)
    {
        if (isSelectionActive)
        {
            Debug.LogWarning("Selection is already active!");
            return;
        }

        requiredSelectionCount = required;
        selectionCompleteCallback = onComplete;
        selectionCancelledCallback = onCancel;
        
        isSelectionActive = true;
        selectedObjects.Clear();
        
        Debug.Log($"Selection started - Select {required} objects");
    }

    public void StopSelection()
    {
        if (isSelectionActive)
        {
            CancelSelection();
        }
    }

    public void ForceCompleteSelection()
    {
        if (isSelectionActive && selectedObjects.Count > 0)
        {
            CompleteSelection();
        }
    }
    
    // GETTERS
    public List<GameObject> GetSelectedObjects()
    {
        return new List<GameObject>(selectedObjects);
    }
    
    public bool IsSelected(GameObject obj)
    {
        return selectedObjects.Contains(obj);
    }
    
    public int GetSelectionCount()
    {
        return selectedObjects.Count;
    }

    public bool IsSelectionActive()
    {
        return isSelectionActive;
    }

    public int GetRequiredSelectionCount()
    {
        return requiredSelectionCount;
    }
}