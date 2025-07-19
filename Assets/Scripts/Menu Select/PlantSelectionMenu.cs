using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlantSelectionMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    private System.Action<PlantData> onPlantSelected;

    public void OpenMenu(List<PlantData> availablePlants, System.Action<PlantData> callback)
    {
        gameObject.SetActive(true);
        onPlantSelected = callback;

        // Limpiar botones anteriores
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // Crear un botón por cada planta disponible
        foreach (PlantData plant in availablePlants)
        {
            GameObject btn = Instantiate(buttonPrefab, buttonContainer);
            btn.GetComponentInChildren<Image>().sprite = plant.icon;
            btn.GetComponent<Button>().onClick.AddListener(() => OnSelectPlant(plant));
        }
    }

    public void OnSelectPlant(PlantData plant)
    {
        onPlantSelected?.Invoke(plant);
        CloseMenu();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
