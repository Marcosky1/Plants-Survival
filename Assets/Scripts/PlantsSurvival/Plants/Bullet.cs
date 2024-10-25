using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    private int damage;

    public void SetTarget(Transform _target, int _damage)
    {
        target = _target;
        damage = _damage;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                Zombie zombieScript = target.GetComponent<Zombie>();
                if (zombieScript != null)
                {
                    zombieScript.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}




