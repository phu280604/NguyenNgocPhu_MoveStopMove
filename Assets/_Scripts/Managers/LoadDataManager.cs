using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadDataManager : Singleton<LoadDataManager>
{
    public T Load<T>(string fileName)
    {
        string path = StringCollection.ROOT_JSON_DATA + $"/{fileName}.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }

        Debug.LogError($"Can't find file name: {fileName}");
        return default;
    }
}
