using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // Reference to Level Editor script
    private LevelEditor levelEditor;

    // Bools stating which control is currently on
    public bool rotationOn;
    public bool scalarOn;
    public bool deleteOn;

    void Start()
    {
        levelEditor = GameObject.FindWithTag("Level Editor").GetComponent<LevelEditor>();
    }

    // When an object is selected, show appropriate controls
    public void OnSelect()
    {
        if (rotationOn)
        {
            ShowRotationControls();
        }
        else if (scalarOn)
        {
            ShowScaleControls();
        }
        else if (deleteOn)
        {
            ShowDeleteButton();
        }
    }

    // When an object is deselected, hide controls
    public void OnDeselect()
    {
        if (!rotationOn)
        {
            HideRotationControls();
        }
        else if (!scalarOn)
        {
            HideScaleControls();
        }
        else if (!deleteOn)
        {
            HideDeleteButton();
        }
    }
    
    // Show rotation controls and select current object
    private void ShowRotationControls()
    {
        levelEditor.rotationPanel.SetActive(true);
        levelEditor.selectedObject = gameObject;
        HideScaleControls();
        HideDeleteButton();
    }

    // Hide rotation controls
    private void HideRotationControls()
    {
        levelEditor.rotationPanel.SetActive(false);
    }

    // Show scale controls and select current object
    private void ShowScaleControls()
    {
        levelEditor.scalarPanel.SetActive(true);
        levelEditor.selectedObject = gameObject;
        HideRotationControls();
        HideDeleteButton();
    }

    // Hide scale controls
    private void HideScaleControls()
    {
        levelEditor.scalarPanel.SetActive(false);
    }

    // Show delete button and select current object
    private void ShowDeleteButton()
    {
        levelEditor.deleteButton.SetActive(true);
        levelEditor.selectedObject = gameObject;
        HideScaleControls();
        HideRotationControls();
    }

    // Hide delete button
    private void HideDeleteButton()
    {
        levelEditor.deleteButton.SetActive(false);

    }
}
