using UnityEngine;

[RequireComponent(typeof(ItemMainHand))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    private ItemMainHand item;

    void Start()
    {
        item = GetComponent<ItemMainHand>();
    }

    void Update()
    {
        if (item.inHand)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Shoot();
        }
    }

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.transform.Rotate(bulletSpawnPoint.forward);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
