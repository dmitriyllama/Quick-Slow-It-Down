using UnityEngine;

public class Gun : ItemMainHand
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootDeltaTime;
    private float lastShootTime = 0;

    public void Shoot(Ray ray)
    {
        if (Time.timeSinceLevelLoad < lastShootTime + shootDeltaTime) return; // Too early to shoot
        
        Vector3 direction;
        if (Physics.Raycast(ray, out var hit))
        {
            direction = hit.point - bulletSpawnPoint.position;
            direction.Normalize();
        }
        else
        {
            direction = bulletSpawnPoint.forward;
        }
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.transform.Rotate(bulletSpawnPoint.forward);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        lastShootTime = Time.timeSinceLevelLoad;
    }
}
