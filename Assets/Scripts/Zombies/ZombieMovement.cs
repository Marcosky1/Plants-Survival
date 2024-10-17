using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    public int damage = 10;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player no encontrado. Asegúrate de que el Player tiene el tag 'Player'.");
        }
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            PlantController plant = collision.gameObject.GetComponent<PlantController>();
            if (plant != null)
            {
                plant.TakeDamage(damage); 
            }
        }
    }
}

