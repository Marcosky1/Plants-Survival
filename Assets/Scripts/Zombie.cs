using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData; 
    private int currentHealth;
    private ObjectPool pool;

    private void OnEnable()
    {

        currentHealth = zombieData.health;
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        pool.ReturnToPool(gameObject);
    }
}


