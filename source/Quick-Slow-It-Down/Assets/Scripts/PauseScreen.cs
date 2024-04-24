using System.Collections;
using System.Collections.Generic;
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
        player.GetComponent<MouseLookX>().enabled = true;
        player.GetComponentInChildren<MouseLookY>().enabled = true;
        player.GetComponent<FPSInput>().enabled = true;
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}