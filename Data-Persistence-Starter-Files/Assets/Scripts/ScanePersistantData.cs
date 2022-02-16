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
    void SortingArray (int [] numbers, string[] names)
    {
        var auxNumbers=new int[numbers.Length]; 
        var auxNames=new string[names.Length];

        for (int i = 0; i < numbers.Length; i++)
        {
            auxNumbers[i]=numbers[i];
            auxNames[i]=names[i];
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            for(int j = 0; j < numbers.Length; j++)
            {
                //en cada vuelta compara la posicion i del array principal de numeros con la posicion j del auxiliar,
                //en caso de ser mayor guarda en esa posicion ese valor y en su equivalente el valor del nombre.
                //Esto se repite hasta recorrer por completo todas las posiciones de i
            }
        }
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
            //compare score here in order to save the best scores
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

            Debug.Log(data.names.Length);

            for (int i = 0; i < data.names.Length; i++)
            {
                names[i] = data.names[i];
                scores[i] = data.scores[i];
                Debug.Log(i + 1 + ". " + names[i] + ":  " + scores[i]);
            }
                        
        }
    }
}
