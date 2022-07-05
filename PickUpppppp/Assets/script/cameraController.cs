using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float MouseSensitivity = 100;
    public GameObject Player;
    private float xRotation = 0f;
    private bool slideLooking = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        CameraUpdate(true);
    }
    public void CameraUpdate(bool sliding)
    {
        //Get Mouse X and Y inputs
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        //set xRotation and clamp it so player can't look directly up or down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        //Set normal camera rotations and rotations for when the player is sliding
        Vector3 slideRotations = new Vector3(xRotation - 8, 0f, 0f);
        Vector3 rotations = new Vector3(xRotation, mouseX, 0f);

        float camXRot = 0;

        //If the player is sliding (Holding shift and their speed is > 6) the camera should rotate up a little to give a sliding effect
        if (sliding)
        {
            slideLooking = true;
            camXRot = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(slideRotations), 7 * Time.deltaTime).eulerAngles.x;
        }

        //If the player is no longer sliding, we need to rotate the camera back
        else
        {
            //We will lerp the camera until it's almost at its destination, and then we will stop
            if (slideLooking)
            {
                camXRot = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(rotations), 7 * Time.deltaTime).eulerAngles.x;

                if (Mathf.Abs(this.transform.localRotation.eulerAngles.x - camXRot) < .001f)
                {
                    slideLooking = false;
                }
            }

            //once we stop lerping the camera, it wil be moved only based on the mouse position again
            else
            {
                camXRot = xRotation;
            }
        }

        this.transform.localRotation = Quaternion.Euler(camXRot, 0f, 0f);
        Player.transform.Rotate(Player.transform.up * mouseX);
    }
}
