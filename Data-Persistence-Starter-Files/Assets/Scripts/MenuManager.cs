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
    // Start is called before the first frame update
    void Start()
    {
        ScanePersistantData.Instance.LoadAll();
        lastBestScore.text = "High Score: " + ScanePersistantData.Instance.scores[0]+ ", " +ScanePersistantData.Instance.names[0];
    }

    public void StartButtonClicked()
    {
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
