using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lastBestScore;
    [SerializeField] TextMeshProUGUI ranking;
    public TextMeshProUGUI nameInputText;
    private string rankingString;
    // Start is called before the first frame update
    void Start()
    {
        ScanePersistantData.Instance.LoadAll();
        LoadRanking();
    }
    private void LoadRanking()
    {
        ranking.text = "";
        rankingString = "";
        lastBestScore.text = "High Score: " + ScanePersistantData.Instance.scores[0] + ", " + ScanePersistantData.Instance.names[0];

        for (int i = 0; i < ScanePersistantData.Instance.scores.Length; i++)
        {
            if (ScanePersistantData.Instance.scores[i] != 0)
            {
                rankingString = rankingString + "" + (i + 1) + ". " + ScanePersistantData.Instance.names[i] + ": " + ScanePersistantData.Instance.scores[i] + "\n";
            }
        }
        ranking.text = rankingString;
        
    }

    public void StartButtonClicked()
    {

        ScanePersistantData.Instance.SaveName(nameInputText.text);
        SceneManager.LoadScene(1);
    }

    public void ExitButtonClicked()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }
    public void DeleteRankingRecords()
    {
        ScanePersistantData.Instance.DeleteRankingRecords();
        ScanePersistantData.Instance.LoadAll();
        LoadRanking();

    }
}
