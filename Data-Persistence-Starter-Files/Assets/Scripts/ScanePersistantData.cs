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
        public int actualScore;
        public string[] names = new string[10];
        public int[] scores = new int[10];
    }
    
    public void SaveAll()
    {
        SaveData data = new SaveData();
        data.actualScore = actualScore;
        var index = 0;
        var nameSaved = false;
        var finished = false;

        

        while (!finished && index < 10)
        {
            if(scores[index] > 0)
            {
                data.scores[index] = scores[index];
                data.names[index] = names[index];
                index++;
            }
            else
            {
                finished = true;
            }
        }

        if (!nameSaved && index < 10) 
        { 
            data.names[index] = actualName.text;
            data.scores[index] = actualScore;
            nameSaved = true; 
        }

        SortArrays(actualScore, actualName.text);
        //data.scores in a for = scores and the same with names
        for (int i = 0; i < scores.Length; i++)
        {
            data.scores[i]= scores[i];
            data.names[i] = names[i];
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

            Debug.Log(data.names.Length);

            for (int i = 0; i < data.names.Length; i++)
            {
                names[i] = data.names[i];
                scores[i] = data.scores[i];
                Debug.Log(i + 1 + ". " + names[i] + ":  " + scores[i]);
            }
                        
        }
    }
    void SortArrays(int score, string name)
    {
        var auxNumbers = new int[scores.Length+1];
        var auxNames = new string[names.Length+1];
        var highScore = 0;
        var highScoreIndex = 0;
        

        for (int i = 0; i < scores.Length; i++)
        {
            auxNumbers[i] = scores[i];
            auxNames[i] = names[i];
        }
        auxNumbers[auxNumbers.Length-1] = score;
        auxNames[auxNames.Length - 1] = name;

        for (int i = 0;i < scores.Length; i++)
        {
            for (int j = 0; j < auxNumbers.Length; j++)
            {
                if (auxNumbers[j] > highScore)
                {
                    highScore = auxNumbers[j];
                    highScoreIndex = j;

                }
            }
            scores[i] = highScore;
            names[i] = auxNames[highScoreIndex];
            auxNumbers[highScoreIndex] = 0;

            highScoreIndex = 0;
            highScore = 0;
            
        }

        
    }
}
