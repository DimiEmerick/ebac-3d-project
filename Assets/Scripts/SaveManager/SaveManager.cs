using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    public int lastLevel;
    
    [SerializeField] private SaveSetup _saveSetup;
    private string _path;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        _path = Application.dataPath + "/save.txt";
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 1;
        _saveSetup.checkpoint = 0;
        _saveSetup.playerName = "Dimi";
    }

    #region SAVE_CONTENT
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int c)
    {
        _saveSetup.lastLevel = 1;
        _saveSetup.checkpoint = c;
        _saveSetup.health = EbacPlayer.Instance.healthBase.currentLife;
        _saveSetup.cloth = EbacPlayer.Instance.clothChanger.currentTexture;
        SaveItems();
        Save();
    } 

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    public void Load()
    {
        string fileLoaded = "";
        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded.Invoke(_saveSetup);
        Debug.Log("File Loaded: " + fileLoaded);
    }

    public void DeleteSave()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
            CreateNewSave();
        }
    }
}

[System.Serializable]
public class SaveSetup
{
    public int checkpoint;
    public int coins;
    public int lifePacks;
    public int lastLevel;
    public float health;
    public string playerName;
    public Texture2D cloth;
}
