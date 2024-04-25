using UnityEngine;

public class Gun : ItemMainHand
{
    private Transform bulletSpawnPoint;
    private Animator animations;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootDeltaTime;
    private float lastShootTime = 0;
    
    private static readonly int Shooting = Animator.StringToHash("Shooting");

    public void Shoot(Ray ray)
    {
        if (!inHand) return;
        if (Time.timeSinceLevelLoad < lastShootTime + shootDeltaTime) return; // Too early to shoot
        
        if (!bulletSpawnPoint) bulletSpawnPoint = transform.GetChild(0);
        if (!animations) animations = GetComponent<Animator>();
        
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
        GetComponent<AudioSource>().Play();
        lastShootTime = Time.timeSinceLevelLoad;
        animations.SetInteger(Shooting, 1);
        animations.SetInteger(Shooting, -1);
    }
}
