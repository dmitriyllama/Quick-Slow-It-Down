using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTarget : MonoBehaviour
{
    private EnemyAI ai;
        
    private Tutorial scene;
    private UnityEvent deathEvent;

    void Start()
    {
        ai = GetComponent<EnemyAI>();
        
        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Tutorial>();
        deathEvent = new UnityEvent();
        deathEvent.AddListener(scene.ReactToEnemyDeath);
    }

    public void ReactToHit()
    {
        deathEvent.Invoke();
        ai.Die();
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}