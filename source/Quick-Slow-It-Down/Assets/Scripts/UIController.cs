using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] PauseScreen pauseScreen;
    
    void Start() {
        pauseScreen.Close();
        DontDestroyOnLoad(this);
    }
    
    public void OnGamePause()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MouseLookX>().enabled = false;
        player.GetComponentInChildren<MouseLookY>().enabled = false;
        player.GetComponent<FPSInput>().enabled = false;
        Time.timeScale = 0;
        
        pauseScreen.Open();
    }
}