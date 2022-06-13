using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int Speed = 100;
    public int zoomSpeed = 40;
    // Update is called once per frame
    private void Start()
    {
        Time.fixedDeltaTime = 0.01f;
        transform.position = new Vector3(20, 20, transform.position.z);
    }
    private void FixedUpdate()
    {
        /*
        This function is called a specific amount of times every second, which can be changed by
        changing the fixedDeltaTime like I did in start.
        */
        movePos(checkInput());
        changeZoom();
    }
    private Vector3 checkInput()
    {
        /*
        Checks the current input values for the horizontal and vertical axes.
        They can be assigned in the Unity project settings. I have them as WASD and the arrow keys.
        */
        float xMove = (float)(Input.GetAxis("Horizontal") * Speed / 100);
        float yMove = (float)(Input.GetAxis("Vertical") * Speed / 100);
        return new Vector3(xMove, yMove, 0);
    }
    private void movePos(Vector3 Move)
    {
        /*
        Literally just changes the postiion of the camera
        */
        transform.position += Move;
    }
    public void changeZoom()
    {
        /*
        Changes the zoom of the camera based on the input detected from the mouse scrollwheel. 
        */
        float scrollDirec = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize += scrollDirec * zoomSpeed / -10;
        //making sure it doesn't go below zero and invert the picture displayed
        if (GetComponent<Camera>().orthographicSize < 1)
        {
            GetComponent<Camera>().orthographicSize = 1;
        }
        // zooming in and out with plus and minus keys
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 1;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 1;
        }
    }
}
