using Controls;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private GameObject player;
    private GameObject artifact;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        artifact = GameObject.FindGameObjectWithTag("Artifact");
    }

    public void Open()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        player.GetComponent<MouseLookX>().enabled = false;
        player.GetComponentInChildren<MouseLookY>().enabled = false;
        player.GetComponent<FPSInput>().enabled = false;
        artifact.GetComponent<TimeArtifact>().enabled = false;
        Time.timeScale = 0f;
    }
    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        player.GetComponent<MouseLookX>().enabled = true;
        player.GetComponentInChildren<MouseLookY>().enabled = true;
        player.GetComponent<FPSInput>().enabled = true;
        artifact.GetComponent<TimeArtifact>().enabled = true;
    }
}