using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OldGameManager : MonoBehaviour
{
    public static OldGameManager Instance; 

    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public GameObject storeUI;
    public PlantData[] allPlants; 
    public PlantButton[] plantButtons;
    public PlayerController player;

    private int suns = 0;

    private int zombiesKilled = 0; 
    private float survivalTime = 0f; 
    private bool isGameOver = false;

    [SerializeField] private TextMeshProUGUI sunsText;


    public int Suns
    {
        get { return suns; }
        set { suns = value; } 
    }

    public bool isStoreOpen = false;
    private GameObject currentPlant;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnZombies());
        StartCoroutine(OpenStoreAfterTime(60f));
        SetInitialPlant();    
    }

    private void Update()
    {
        UpdateSunsUI();
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
        }
    }
    void UpdateSunsUI()
    {
        if (sunsText != null)
        {
            sunsText.text = $"Soles: {suns}";
        }
    }

    void SetInitialPlant()
    {
        PlantData initialPlantData = allPlants[0]; 
        currentPlant = Instantiate(initialPlantData.plantPrefab, player.transform);
        currentPlant.transform.localPosition = Vector3.zero; 
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(zombiePrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator OpenStoreAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        PauseGameAndOpenStore();
    }

    public void PauseGameAndOpenStore()
    {
        Time.timeScale = 0f;
        isStoreOpen = true;
        storeUI.SetActive(true);

        PlantData[] randomPlants = GetRandomPlants(3);
        for (int i = 0; i < plantButtons.Length; i++)
        {
            plantButtons[i].SetPlant(randomPlants[i]);
        }
    }

    PlantData[] GetRandomPlants(int count)
    {
        PlantData[] randomPlants = new PlantData[count];
        for (int i = 0; i < count; i++)
        {
            randomPlants[i] = allPlants[Random.Range(0, allPlants.Length)];
        }
        return randomPlants;
    }

    public void CloseStore()
    {
        storeUI.SetActive(false);
        Time.timeScale = 1f;
        isStoreOpen = false;
    }

    public void AddSuns(int amount)
    {
        suns += amount;
        Debug.Log($"Soles totales: {suns}");
    }

    public void ChangePlant(PlantData newPlant)
    {
        if (currentPlant != null)
        {
            Destroy(currentPlant); 
        }

        currentPlant = Instantiate(newPlant.plantPrefab, player.transform); 
        currentPlant.transform.localPosition = Vector3.zero;
    }

    public void AddZombieKill()
    {
        zombiesKilled++;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; 
        ShowGameOverScreen();
    }

    void ShowGameOverScreen()
    {
        Debug.Log($"Game Over! Zombis eliminados: {zombiesKilled}, Tiempo sobrevivido: {survivalTime} segundos");
        SceneManager.LoadScene("GameOver"); 
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

    public float GetSurvivalTime()
    {
        return survivalTime;
    }
}




