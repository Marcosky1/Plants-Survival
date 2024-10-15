using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Game/Resource")]
public class Resource : ScriptableObject
{
    public int currentPoints;
    public int totalPoints;

    public void AddPoints(int amount)
    {
        currentPoints += amount;
        totalPoints += amount;
    }

    public void ResetPoints()
    {
        currentPoints = 0;
    }
}
