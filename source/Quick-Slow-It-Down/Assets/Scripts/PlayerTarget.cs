using Controls;
using SceneScripts;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTarget : MonoBehaviour
{
    public bool alive { get; private set; }

    private Level level;
    private UnityEvent deathEvent;
    
    void Start()
    {
        alive = true;
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
        deathEvent = new UnityEvent();
        deathEvent.AddListener(level.ReactToPlayerDeath);
    }

    public void ReactToHit()
    {
        if (alive)
        {
            alive = false;
            transform.Rotate(-75, 0, 0);
            GetComponent<FPSInput>().enabled = false;
            deathEvent.Invoke();
        }
    }
}