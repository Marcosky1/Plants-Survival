using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Peashooter : MonoBehaviour
{
    public PlantData plantData; 
    public Transform firePoint; 
    public GameObject bulletPrefab; 
    public Text plantNameText; 
    public LayerMask zombieLayer; 
    public float range = 5f; 

    private Zombie targetZombie; 
    private float fireRate = 2f; 
    private float nextFireTime = 0f;

    private void Start()
    {
        plantNameText.text = plantData.plantName; 
    }

    private void Update()
    {
        Collider2D[] zombiesInRange = Physics2D.OverlapCircleAll(transform.position, range, zombieLayer);
        if (zombiesInRange.Length > 0)
        {
            targetZombie = zombiesInRange[0].GetComponent<Zombie>();

            if (targetZombie != null && Time.time >= nextFireTime)
            {
                Shoot(targetZombie);
                nextFireTime = Time.time + fireRate; 
            }
        }
    }

    private void Shoot(Zombie zombie)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(zombie, plantData.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

