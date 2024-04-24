using SceneScripts;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTarget : MonoBehaviour
{
    private Level level;
    private UnityEvent deathEvent;
    
    void Start()
    {
        level = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level>();
        deathEvent = new UnityEvent();
        deathEvent.AddListener(level.ReactToPlayerDeath);
    }

    public void ReactToHit()
    {
        deathEvent.Invoke();
    }
}