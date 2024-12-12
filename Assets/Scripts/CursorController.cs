using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    bool isCursorLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        isCursorLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            toggleCursorState();
        }
    }

    public void toggleCursorState()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            isCursorLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            isCursorLocked = true; 
        }
    }
}
