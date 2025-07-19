using System.Collections.Generic;
using UnityEngine;

public class PlantSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public Transform spawnPoint;

    public void TryOpenPlantMenu(List<PlantData> availablePlants, PlantSelectionMenu menu)
    {
        if (isOccupied) return;

        menu.OpenMenu(availablePlants, (selectedPlant) =>
        {
            GameObject instance = Instantiate(selectedPlant.prefab, spawnPoint.position, Quaternion.identity);
            instance.transform.SetParent(transform);
            isOccupied = true;
        });
    }
}
