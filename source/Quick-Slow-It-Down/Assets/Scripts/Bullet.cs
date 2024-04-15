using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private float bulletLife;

    private Coroutine hitReg;
    
    void Awake()
    {
        Destroy(gameObject, bulletLife);
        hitReg = StartCoroutine(HitReg());
    }

    private IEnumerator HitReg()
    {
        yield return new WaitForSeconds(0.05f);
        while (transform)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * -1, out hit, 1))
            {
                Hit(hit.transform);
            }
            yield return new WaitForFixedUpdate();
        }
    } 

    void OnCollisionEnter(Collision other)
    {
        Hit(other.transform);
    }

    void Hit(Transform other)
    {
        StopCoroutine(hitReg);
        var reactiveTarget = other.GetComponent<EnemyTarget>();
        var player = other.GetComponent<PlayerTarget>();
        if (reactiveTarget)
        {
            reactiveTarget.ReactToHit();
        }
        else if (player)
        {
            player.ReactToHit();
        }
        Instantiate(smokePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
