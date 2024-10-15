using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "Plants/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public int damage;
    public float fireRate;
    public GameObject projectilePrefab;
    public PlantType plantType;


    public enum PlantType
    {
        Normal,
        Fire,
        Ice,
        Electric,
        MultiShot
    }
}

