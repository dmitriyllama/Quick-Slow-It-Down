using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
            canvas.OnGamePause();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
    }
}
