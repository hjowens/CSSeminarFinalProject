using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int Speed = 5;
    // Update is called once per frame
    void Update()
    {
        movePos(checkInput());
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
}
