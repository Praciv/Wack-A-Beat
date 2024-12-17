using UnityEngine;

public class CursorController : MonoBehaviour
{
    bool isCursorLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // turns the cursor on by default 
        Cursor.lockState = CursorLockMode.None;
        isCursorLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Let's the user turn off or on the cursor by pressing the c key
        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleCursorState();
        }
    }

    //Function to flip the state of the cursor mode when called
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
