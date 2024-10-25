using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    public PlantData plantData; 
    public Transform shootPoint; 
    private float shootTimer;
    private int currentHealth;

    public TextMeshProUGUI healthText;
    public Slider healthSlider;

    private void Start()
    {
        currentHealth = plantData.health;
        UpdateHealthUI();
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= plantData.shootRate)
        {
            Shoot();
            shootTimer = 0f;
        }
        UpdateHealthUI();
    }

    void Shoot()
    {
        Collider2D[] hitZombies = Physics2D.OverlapCircleAll(transform.position, plantData.shootRange);

        foreach (Collider2D hit in hitZombies)
        {
            if (hit.CompareTag("Zombie"))
            {
                Bullet bullet = ObjectPool.Instance.GetBullet();

                bullet.transform.position = shootPoint.position;

                bullet.SetTarget(hit.transform, plantData.damage);

                break; 
            }
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Vida: {currentHealth}";
        }

        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / plantData.health; 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, plantData.shootRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}



