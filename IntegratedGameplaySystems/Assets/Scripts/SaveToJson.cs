using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveToJson<T>
{
    [SerializeField] private SaveData<T> scoreSaveData = new();

    private string fileName;
    public SaveToJson(string fileName)
    {
        this.fileName = fileName;
    }

    public void SaveObjectToJson(T value)
    {
        scoreSaveData.saveData = value;

        string saveDataString = JsonUtility.ToJson(scoreSaveData);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.json", saveDataString);
        Debug.Log("Saved Data");
    }

    public T ReturnSavedInt()
    {
        if (!File.Exists(Application.persistentDataPath + $"/{fileName}.json")) { return default; }

        string filePath = Path.Combine(Application.persistentDataPath + $"/{fileName}.json");
        string data = File.ReadAllText(filePath);
        SaveData<T> scoreSaveData = JsonUtility.FromJson<SaveData<T>>(data);

        return scoreSaveData.saveData;
    }
}

[System.Serializable]
public class SaveData<T>
{
    public T saveData;
}