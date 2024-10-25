using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject bulletPrefab; 
    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        Instance = this;

        bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            maxSize: 50);  
    }

    public Bullet GetBullet()
    {
        return bulletPool.Get();
    }

    public void ReturnBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    private Bullet CreateBullet()
    {
        GameObject bulletObject = Instantiate(bulletPrefab);  
        return bulletObject.GetComponent<Bullet>();
    }

    private void ActivateBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void DeactivateBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}



