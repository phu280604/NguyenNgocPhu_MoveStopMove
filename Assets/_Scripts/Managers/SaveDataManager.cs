using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataManager : Singleton<SaveDataManager>
{
    public void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true); // true = format dễ đọc
        string path = StringCollection.ROOT_JSON_DATA + $"/{fileName}.json";
        Debug.Log(json);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }
}
