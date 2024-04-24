using Controls;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var artifact = GameObject.FindGameObjectWithTag("Artifact");
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        player.GetComponent<MouseLookX>().enabled = true;
        player.GetComponentInChildren<MouseLookY>().enabled = true;
        player.GetComponent<FPSInput>().enabled = true;
        artifact.GetComponent<TimeArtifact>().enabled = true;
        
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        gameObject.SetActive(false);
    }
}