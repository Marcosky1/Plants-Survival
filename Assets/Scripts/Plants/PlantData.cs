using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "Plants/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public int damage;
    public int health;
    public float shootRate;
    public float shootRange;
    public GameObject plantPrefab; 
    public int cost;
    
    public Sprite icon;
    public GameObject prefab;
}


