using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    private Zombie target;
    private int damage;

    public void SetTarget(Zombie zombie, int plantDamage)
    {
        target = zombie;
        damage = plantDamage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject); 
    }
}


