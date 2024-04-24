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
        alive = false;
        deathEvent.Invoke();
    }
}