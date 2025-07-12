using UnityEngine;
using TMPro; 

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text zombiesKilledText;
    public TMP_Text survivalTimeText;

    private void Start()
    {
        int zombiesKilled = OldGameManager.Instance.GetZombiesKilled();
        float survivalTime = OldGameManager.Instance.GetSurvivalTime();

        zombiesKilledText.text = $"Zombis Eliminados: {zombiesKilled}";
        survivalTimeText.text = $"Tiempo Sobrevivido: {survivalTime:F2} segundos";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

