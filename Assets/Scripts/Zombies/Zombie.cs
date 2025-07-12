using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health = 100;
    public int sunsReward = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Zombi recibió {damage} de daño. Salud restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OldGameManager.Instance.AddSuns(sunsReward);
        OldGameManager.Instance.AddZombieKill();
        Debug.Log("El zombi ha muerto.");
        Destroy(gameObject);
    }
}
