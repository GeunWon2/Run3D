using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;

    public SaveState save;
    private const string saveFileName = "data.ss";
    private BinaryFormatter formatter;

    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;

    private void Awake()
    {
        instance = this;
        formatter = new BinaryFormatter();

        Load();
    }

    private void Load()
    {
        try
        {
            FileStream file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            save = (SaveState)formatter.Deserialize(file);
            file.Close();
            OnLoad?.Invoke(save);
        }

        catch
        {
            Debug.Log("Save file not found!!");
            Save();
        }
    }

    public void Save()
    {
        if(save == null)
        {
            save = new SaveState();
        }

        save.LastSaveTime = DateTime.Now;

        FileStream file = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, save);
        file.Close();

        OnSave?.Invoke(save);
    }
}
