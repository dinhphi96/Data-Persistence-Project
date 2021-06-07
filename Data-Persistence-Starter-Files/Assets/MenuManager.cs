using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public int BestcoreSaveMenu = 0;
    public string NameSaveMenu = "Null";
    public string NameBestCoreMenu;
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadCore();
    }
    [System.Serializable]
    class SaveData
    {
        //public Color TeamColor1;
        public int BestcoreSave = 0;
        public string NameSave = "Null";
        public string NameSaveBestCore;
    }

    public void SaveCore()
    {
        SaveData data = new SaveData();
        data.BestcoreSave = BestcoreSaveMenu;
        data.NameSave = NameSaveMenu;
        data.NameSaveBestCore = NameBestCoreMenu;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadCore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //SaveData data1 = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/savefile.json"));
            BestcoreSaveMenu = data.BestcoreSave;
            NameSaveMenu = data.NameSave;
            NameBestCoreMenu = data.NameSaveBestCore;
        }
    }
}
