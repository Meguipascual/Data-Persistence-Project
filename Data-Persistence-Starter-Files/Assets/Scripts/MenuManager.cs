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
    private string rankingString;
    // Start is called before the first frame update
    void Start()
    {
        ScanePersistantData.Instance.LoadAll();
        for (int i = 0; i < ScanePersistantData.Instance.scores.Length; i++)
        {
            rankingString = rankingString + ""+ (i+1) + ". "+ ScanePersistantData.Instance.names[i] + ": " + ScanePersistantData.Instance.scores[i] + "\n";
        }
        ranking.text = rankingString;
        lastBestScore.text = "High Score: " + ScanePersistantData.Instance.scores[0]+ ", " +ScanePersistantData.Instance.names[0];
    }

    public void StartButtonClicked()
    {
        ScanePersistantData.Instance.SaveName();
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
}
