using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 1f;
    public int health = 3;
    private float amount;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);  // En lugar de destruir el objeto, lo desactivamos
    }

    public void Slow(float amount, float duration)
    {
        speed *= amount;
        StartCoroutine(ResetSpeed(duration));
    }

    IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed /= amount;
    }
}

