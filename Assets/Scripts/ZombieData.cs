using UnityEngine;

[CreateAssetMenu(fileName = "NewZombie", menuName = "Zombies/ZombieData")]
public class ZombieData : ScriptableObject
{
    public string zombieName;
    public int health;
    public float speed;
    public ZombieType zombieType;

    public enum ZombieType
    {
        Normal,
        Cone,
        Reader,
        Brick
    }
}

