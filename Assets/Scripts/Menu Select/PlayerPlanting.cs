using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    public List<PlantData> myAvailablePlants;
    public PlantSelectionMenu plantMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // O botón de interacción
        {
            TryInteractWithMaceta();
        }
    }

    void TryInteractWithMaceta()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.5f);
        if (hit.collider != null)
        {
            PlantSlot slot = hit.collider.GetComponent<PlantSlot>();
            if (slot != null)
            {
                slot.TryOpenPlantMenu(myAvailablePlants, plantMenu);
            }
        }
    }
}

