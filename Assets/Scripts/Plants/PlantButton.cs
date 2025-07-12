using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour
{
    public PlantData plantData;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetPlant(PlantData newPlant)
    {
        plantData = newPlant;
    }

    private void OnButtonClick()
    {
        if (OldGameManager.Instance.Suns >= plantData.cost) 
        {
            OldGameManager.Instance.Suns -= plantData.cost; 
            OldGameManager.Instance.ChangePlant(plantData); 
            OldGameManager.Instance.CloseStore(); 
        }
        else
        {
            Debug.Log("No tienes suficientes soles para comprar esta planta.");
        }
    }
}


