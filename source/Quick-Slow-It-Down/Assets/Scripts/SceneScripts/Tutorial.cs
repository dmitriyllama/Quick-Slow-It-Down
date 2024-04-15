using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GUIStyle guiStyle;
    private string label;
    private Camera cam;

    private Coroutine activeCoroutine;
    private int progress;
    private bool readThroughEnoughTutorial;
    
    // TODO All of this should be restructured

    [SerializeField] private Transform opposingGun;
    [SerializeField] private Transform enemy;
    public void ReactToPlayerAction()
    {
        if (progress != 0) return;
        progress++;
        
        label = "";
        
        StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(ActivateEnemy());
    }

    public void ReactToPlayerDeath()
    {
        StopCoroutine(activeCoroutine);
        StartCoroutine(ResetTutorial());
    }
    
    public void ReactToEnemyDeath()
    {
        if (progress != 1) return;
        progress++;
        
        StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(EndTutorial());
    }
    
    private IEnumerator StartTutorial()
    {
        label = "Don't move!";
        yield return new WaitForSecondsRealtime(4.2f);
        guiStyle.fontSize = 24;
        label = "Don't do anything, or you get shot.";
        yield return new WaitForSecondsRealtime(3.7f);
        label = "And don't panic, this is a test.";
        yield return new WaitForSecondsRealtime(3.7f);
        label = "Now listen. You have to follow the one rule of this bizarre game.";
        yield return new WaitForSecondsRealtime(4.2f);
        label = "In front of you is a ball of shining rock, which we'll have to call the \"time orb\".";
        yield return new WaitForSecondsRealtime(4f);
        label = "A time orb will slow down everything around you if you push on it.";
        yield return new WaitForSecondsRealtime(6.2f);
        label = "Also, there's a gun. It should un-alive the other guy if you use it wisely.";
        yield return new WaitForSecondsRealtime(4.2f);
        label = "The rule here is simple. You're doomed if you try to reach the gun first.";
        yield return new WaitForSecondsRealtime(4.7f);
        label = "You might have to use the time orb instead.";
        yield return new WaitForSecondsRealtime(4.7f);
        label = "Focus now. You have to be quick.";
        readThroughEnoughTutorial = true;
        yield return new WaitForSecondsRealtime(3.2f);
        label = "Grab an item [E]\nSlow it all down [Right Click]\nShoot the guy! [Left Click]";
        yield return new WaitForSecondsRealtime(10f);
    }

    private IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        
        var hand = enemy.GetChild(1);
        opposingGun.SetParent(hand);
        opposingGun.GetComponent<Rigidbody>().isKinematic = true;
        opposingGun.localPosition = Vector3.zero;
        opposingGun.localEulerAngles = Vector3.zero;
        
        yield return new WaitForSeconds(0.3f);

        var gun = opposingGun.GetComponent<Gun>();
        while (true)
        {
            gun.Shoot();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator ResetTutorial()
    {
        yield return new WaitForSecondsRealtime(1.7f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(3.2f);
        if (readThroughEnoughTutorial)
        {
            label = "There you go, bastard!";
            yield return new WaitForSecondsRealtime(3.2f);
        }
        else
        {
            guiStyle.fontSize = 24;
            label = "Woah";
            yield return new WaitForSecondsRealtime(2.2f);
            label = "Quick enough!";
            yield return new WaitForSecondsRealtime(2.9f);
        }
        label = "";
        Debug.Log("You win!");
    }
    
    void Start()
    {
        cam = Camera.main;
        activeCoroutine = StartCoroutine(StartTutorial());
    }

    private void OnGUI()
    {
        int w = 500;
        int h = 100;
        float posX = (cam.pixelWidth - w) / 2;
        float posY = (cam.pixelHeight) / 4;
        GUI.Label(new Rect(posX, posY, w, h), label, guiStyle);
    }
}
