using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLevel : MonoBehaviour
{
    private string path = "";
    private string persistentPath = "";

    // Set path of file and use inputed file name
    public void SetPaths(string fileName)
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + fileName;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName;
    }

    // Get names of all previously saved levels
    public string[] GetSavedLevelNames()
    {
        string[] fileArray = Directory.GetFiles(Application.persistentDataPath + Path.AltDirectorySeparatorChar, "*.json");
        string[] fileArrayNames = new string[fileArray.Length];
        for (int i=0; i < fileArray.Length; i++)
        {
            string fileName = Path.GetFileName(fileArray[i]);
            fileArrayNames[i] = fileName;
        }
        return fileArrayNames;
    }

    // Save LevelData into file using json 
    public void Save(LevelData levelData)
    {
        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(levelData);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    // Load LevelData by parsing from json file
    public LevelData Load()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        LevelData levelData = JsonUtility.FromJson<LevelData>(json);
        return levelData;
    }
}

