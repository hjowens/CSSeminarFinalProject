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
        movePos(checkInput());
        changeZoom();
    }
    private Vector3 checkInput()
    {
        float xMove = (float)(Input.GetAxis("Horizontal") * Speed / 100);
        float yMove = (float)(Input.GetAxis("Vertical") * Speed / 100);
        return new Vector3(xMove, yMove, 0);
    }
    private void movePos(Vector3 Move)
    {
        transform.position += Move;
    }
    public void changeZoom()
    {
        float scrollDirec = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize += scrollDirec * zoomSpeed / -10;
        if (GetComponent<Camera>().orthographicSize < 1)
        {
            GetComponent<Camera>().orthographicSize = 1;
        }

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
