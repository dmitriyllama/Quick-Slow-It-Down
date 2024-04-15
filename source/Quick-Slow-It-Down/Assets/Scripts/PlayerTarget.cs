using UnityEngine;
using UnityEngine.Events;

public class PlayerTarget : MonoBehaviour
{
    private Tutorial scene;
    private UnityEvent deathEvent;
    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Tutorial>();
        deathEvent = new UnityEvent();
        deathEvent.AddListener(scene.ReactToPlayerDeath);
    }

    public void ReactToHit()
    {
        deathEvent.Invoke();
        
    }
}