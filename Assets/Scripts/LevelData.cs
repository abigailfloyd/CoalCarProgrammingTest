using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData 
{
    // Holds all data about current level: objects and their respective positions, rotations, and scales
    public List<string> objectTypes = new List<string>();
    public List<Vector3> positions = new List<Vector3>();
    public List<Vector3> rotations = new List<Vector3>();
    public List<Vector3> scales = new List<Vector3>();
}
