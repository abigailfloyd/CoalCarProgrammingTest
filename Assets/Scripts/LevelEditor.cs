using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] Transform head;

    // Object prefabs
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] GameObject cylinderPrefab;

    // UI Objects
    public GameObject rotationPanel;
    public GameObject scalarPanel;
    public GameObject deleteButton;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject savesOption;
    [SerializeField] TMP_Dropdown savesDropdown;
    [SerializeField] TMP_InputField textField;
    [SerializeField] TMP_Text xRotText;
    [SerializeField] Slider xRotSlider;
    [SerializeField] TMP_Text yRotText;
    [SerializeField] Slider yRotSlider;
    [SerializeField] TMP_Text zRotText;
    [SerializeField] Slider zRotSlider;
    [SerializeField] TMP_Text xScaleText;
    [SerializeField] Slider xScaleSlider;
    [SerializeField] TMP_Text yScaleText;
    [SerializeField] Slider yScaleSlider;
    [SerializeField] TMP_Text zScaleText;
    [SerializeField] Slider zScaleSlider;
    [SerializeField] Slider uniformScaleSlider;
    

    // References to other scripts
    [SerializeField] XRInteractionManager interactionManager;
    [SerializeField] LevelData levelData;
    [SerializeField] SaveLevel saveLevel;

    // Current object being edited
    public GameObject selectedObject;

    // All objects in current level
    private List<GameObject> allObjects = new List<GameObject>();
   
    void Update()
    {
        // Toggle level editor menu
        if (Input.GetKeyDown("space"))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
        
        // Show editor menu in front of player at all times
        if (canvas.activeSelf)
        {
            canvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 2;
            canvas.transform.LookAt(new Vector3(head.position.x, canvas.transform.position.y, head.position.z));
            canvas.transform.forward *= -1;
        }

        // If rotatation or scalar is selected, display sliders and values
        if (selectedObject != null)
        {
            if (selectedObject.GetComponent<Interactable>().rotationOn)
            {
                xRotText.text = "X: " + (int)xRotSlider.value;
                yRotText.text = "Y: " + (int)yRotSlider.value;
                zRotText.text = "Z: " + (int)zRotSlider.value;
            }
            else if (selectedObject.GetComponent<Interactable>().scalarOn)
            {
                xScaleText.text = "X: " + (int)xScaleSlider.value;
                yScaleText.text = "Y: " + (int)yScaleSlider.value;
                zScaleText.text = "Z: " + (int)zScaleSlider.value;
            }
        }
    }

    // Spawn a cube in front of player
    // Add cube to array of all objects
    public void SpawnCube()
    {
        Vector3 spawnPoint = head.position + head.forward;
        spawnPoint.y = .1f;
        GameObject cube = Instantiate(cubePrefab, spawnPoint, Quaternion.identity);
        cube.GetComponent<XRGrabInteractable>().interactionManager = interactionManager;
        allObjects.Add(cube);
    }

    // Spawn a sphere in front of player
    // Add sphere to array of all objects
    public void SpawnSphere()
    {
        Vector3 spawnPoint = head.position + head.forward;
        spawnPoint.y = .1f;
        GameObject sphere = Instantiate(spherePrefab,spawnPoint, Quaternion.identity);
        sphere.GetComponent<XRSimpleInteractable>().interactionManager = interactionManager;
        allObjects.Add(sphere);
    }

    // Spawn a cylinder in front of player
    // Add cylinder to array of all objects
    public void SpawnCylinder()
    {
        Vector3 spawnPoint = head.position + head.forward;
        spawnPoint.y = .1f;
        GameObject cylinder = Instantiate(cylinderPrefab,spawnPoint, Quaternion.identity);
        cylinder.GetComponent<XRSimpleInteractable>().interactionManager = interactionManager;
        allObjects.Add(cylinder);
    }

    // Enable movement of objects
    // Turn off simple selecting, which is used for rotation and scaling
    // Turn on grab selecting
    public void EnableMovement()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.GetComponent<XRSimpleInteractable>().enabled = false;
            obj.GetComponent<XRGrabInteractable>().enabled = true;
            obj.GetComponent<Interactable>().rotationOn = false;
            obj.GetComponent<Interactable>().scalarOn = false;
            obj.GetComponent<Interactable>().deleteOn = false;
        }
        
    }

    // Enable rotation of objects
    // Turn off grab selecting, which is used for movement
    // Turn on simple selecting
    public void EnableRotation()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<XRSimpleInteractable>().enabled = true;
            obj.GetComponent<Interactable>().rotationOn = true;
            obj.GetComponent<Interactable>().scalarOn = false;
            obj.GetComponent<Interactable>().deleteOn = false;
        }
    }

    // Enable scaling of objects
    // Turn off grab selecting, which is used for movement
    // Turn on simple selecting
    public void EnableScalar()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<XRSimpleInteractable>().enabled = true;
            obj.GetComponent<Interactable>().rotationOn = false;
            obj.GetComponent<Interactable>().scalarOn = true;
            obj.GetComponent<Interactable>().deleteOn = false;
        }
    }

    // Enable deletion of objects
    // Turn on simple selecting and turn off grabbing selecting
    public void EnableDelete()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.GetComponent<XRGrabInteractable>().enabled = false;
            obj.GetComponent<XRSimpleInteractable>().enabled = true;
            obj.GetComponent<Interactable>().rotationOn = false;
            obj.GetComponent<Interactable>().scalarOn = false;
            obj.GetComponent<Interactable>().deleteOn = true;
        }
    }

    // Use slider to change X rotation of object
    public void ChangeXRotation()
    {
        if (selectedObject != null)
        {
            Vector3 newRotation = new Vector3(xRotSlider.value, selectedObject.transform.eulerAngles.y, selectedObject.transform.eulerAngles.z);
            selectedObject.transform.eulerAngles = newRotation;
        }
    }

    // Use slider to change Y rotation of object
    public void ChangeYRotation()
    {
        if (selectedObject != null)
        {
            Vector3 newRotation = new Vector3(selectedObject.transform.eulerAngles.x, yRotSlider.value, selectedObject.transform.eulerAngles.z);
            selectedObject.transform.eulerAngles = newRotation;
        }
    }

    // Use slider to change Z rotation of object
    public void ChangeZRotation()
    {
        if (selectedObject != null)
        {
            Vector3 newRotation = new Vector3(selectedObject.transform.eulerAngles.x, selectedObject.transform.eulerAngles.y, zRotSlider.value);
            selectedObject.transform.eulerAngles = newRotation;
        }
    }

    // Use slider to change X scale of object
    public void ChangeXScale()
    {
        if (selectedObject != null)
        {
            float scaleValue = xScaleSlider.value;
            Vector3 newScale = new Vector3(scaleValue, selectedObject.transform.localScale.y, selectedObject.transform.localScale.z);
            selectedObject.transform.localScale = newScale;
        }
    }

    // Use slider to change Y scale of object
    public void ChangeYScale()
    {
        if (selectedObject != null)
        {
            float scaleValue = yScaleSlider.value;
            Vector3 newScale = new Vector3(selectedObject.transform.localScale.x, scaleValue, selectedObject.transform.localScale.z);
            selectedObject.transform.localScale = newScale;
        }
    }

    // Use slider to change Z scale of object
    public void ChangeZScale()
    {
        if (selectedObject != null)
        {
            float scaleValue = zScaleSlider.value;
            Vector3 newScale = new Vector3(selectedObject.transform.localScale.x, selectedObject.transform.localScale.y, scaleValue);
            selectedObject.transform.localScale = newScale;
        }
    }

    // Use slider to change uniform scale of object
    public void ChangeScaleUniform()
    {
        if (selectedObject != null)
        {
            float scaleValue = uniformScaleSlider.value;
            Vector3 newScale = new Vector3(scaleValue, scaleValue, scaleValue);
            selectedObject.transform.localScale = newScale;
        }
    }

    // Destroy object and remove it from list of all objects in level
    public void DeleteObject()
    {
        if (selectedObject != null)
        {
            allObjects.Remove(selectedObject);
            Destroy(selectedObject);
        }
    }

    // Save current level
    public void Save()
    {
        // Add all objects in current level to LevelData
        for (int i=0; i<allObjects.Count; i++)
        {
            bool isObject = true;
            if (allObjects[i].tag == "Cube")
            {
                levelData.objectTypes.Add("Cube");
            }
            else if (allObjects[i].tag == "Sphere")
            {
                levelData.objectTypes.Add("Sphere");
            }
            else if (allObjects[i].tag == "Cylinder")
            {
                levelData.objectTypes.Add("Cylinder");
            }
            else
            {
                isObject = false;
            }
            if (isObject)
            {
                levelData.positions.Add(allObjects[i].transform.position);
                levelData.rotations.Add(allObjects[i].transform.eulerAngles);
                levelData.scales.Add(allObjects[i].transform.localScale);
            }
        }
        // Use text field to save level under specific name
        saveLevel.SetPaths(textField.text+".json");
        saveLevel.Save(levelData);
    }

    // Load previously created level
    public void Load()
    {
        // Choose level from dropdown
        saveLevel.SetPaths(savesDropdown.options[savesDropdown.value].text);
        // Destroy all objects in current level
        for (int i=0; i<allObjects.Count; i++)
        {
            Destroy(allObjects[i]);
        }
        allObjects.Clear();

        // Load level and create each object in loaded level at correct position, rotation, and scale
        LevelData level = saveLevel.Load();
        for (int i = 0; i < level.objectTypes.Count; i++)
        {
            if (level.objectTypes[i] == "Cube")
            {
                GameObject cube = Instantiate(cubePrefab, level.positions[i], Quaternion.identity);
                cube.transform.eulerAngles = level.rotations[i];
                cube.transform.localScale = level.scales[i];
                allObjects.Add(cube);
            }
            else if (level.objectTypes[i] == "Sphere")
            {
                GameObject sphere = Instantiate(spherePrefab, level.positions[i], Quaternion.identity);
                sphere.transform.eulerAngles = level.rotations[i];
                sphere.transform.localScale = level.scales[i];
                allObjects.Add(sphere);
            }
            else if(level.objectTypes[i] == "Cylinder")
            {
                GameObject cylinder = Instantiate(cylinderPrefab, level.positions[i], Quaternion.identity);
                cylinder.transform.eulerAngles = level.rotations[i];
                cylinder.transform.localScale = level.scales[i];
                allObjects.Add(cylinder);
            }
        }
    }

    // Show all previously saved levels in dropdown menu
    public void showLoads()
    {
        savesOption.SetActive(true);
        List<string> saves = new List<string>();
        foreach (string fileName in saveLevel.GetSavedLevelNames())
        {
            saves.Add(fileName);
        }
        savesDropdown.AddOptions(saves);
    }


}
