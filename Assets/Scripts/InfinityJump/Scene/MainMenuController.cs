using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField playerNameInput; 
    public GameObject newRecordText; 
    private string playerName;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Jugador");
        playerNameInput.text = playerName;
    }

    public void PlayGame()
    {

        playerName = playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameJump"); 
    }

    public void ShowScores()
    {
        SceneManager.LoadScene("ScoresScene"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CheckNewRecord(bool isNewRecord)
    {
        newRecordText.SetActive(isNewRecord);
    }
}
