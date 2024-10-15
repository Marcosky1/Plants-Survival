using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Resource playerPoints;

    public void AwardPoints(int amount)
    {
        playerPoints.AddPoints(amount);
        Debug.Log("Puntos actuales: " + playerPoints.currentPoints);
    }
}
