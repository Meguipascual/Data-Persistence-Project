using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ScanePersistantData : MonoBehaviour
{
    public static ScanePersistantData Instance;
    public TextMeshProUGUI actualName;
    public int actualScore;
    public string[] names = new string[10];
    public int[] scores = new int[10];

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData
    {
        public TextMeshProUGUI actualName;
        public int actualScore;
        public string[] names = new string[10];
        public int[] scores = new int[10];
    }
    public void SaveAll()
    {
        SaveData data = new SaveData();
        data.actualName = actualName;
        data.actualScore = actualScore;
        var index = 0;
        var nameSaved = false;

        while (data.names[index] != null && index < 10)
        {
            //compare score here in order to save the best scores 
            index++;
        }

        if (!nameSaved && index < 10) 
        { 
            data.names[index] = actualName.text;
            data.scores[index] = actualScore;
            nameSaved = true; 
        }


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefileBrick.json", json);
    }

    public void LoadAll()
    {
        string path = Application.persistentDataPath + "/savefileBrick.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            var index = 0;

            while (data.names[index] != null && index<10) 
            {
                names[index] = data.names[index];
                scores[index] = data.scores[index];
                Debug.Log(index+ ". " + names[index] + ":  " + scores[index] + "\n");

                index++;
            }
            
        }
    }
}
