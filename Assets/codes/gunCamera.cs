using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCamera : MonoBehaviour
{
    // Camera references
    public GameObject cam;
    public GameObject mainCam;

    // Rotation settings
    public float horizontalSensitivity = 50f;
    public float verticalSensitivity = 50f;
    private int defCamYPosition = 0;

    // Rotation limits
    public float maxVerticalAngle = 20f; // Maximum upward angle
    public float minVerticalAngle = -20f; // Maximum downward angle
    public float maxHorizontalAngle = 30f; // Maximum rightward angle
    public float minHorizontalAngle = -30f; // Maximum leftward angle

    private bool gunCamView = false;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    private bool gameEnded = false;

    void Start()
    {
        // Ensure the main camera is active initially
        cam.SetActive(false);
        mainCam.SetActive(true);
    }

    void Update()
    {
        // Toggle between gun camera and main camera
        if (Input.GetKeyDown(KeyCode.Mouse1) && gameEnded == false)
        {
            gunCamView = !gunCamView; // Toggle the camera state

            if (!gunCamView)
            {
                // Reset horizontal rotation and set the default camera position when exiting gun camera view
                horizontalRotation = 0f;
                verticalRotation = 0f;
                cam.transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
            }

            cam.SetActive(gunCamView);
            mainCam.SetActive(!gunCamView);
        }



        // Rotate the gun camera when it's active
        if (gunCamView)
        {
            HandleGunCameraRotation();
        }
    }

    void HandleGunCameraRotation()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        // Update horizontal rotation with clamping
        horizontalRotation += mouseX;
        horizontalRotation = Mathf.Clamp(horizontalRotation, minHorizontalAngle, maxHorizontalAngle);

        // Update vertical rotation with clamping
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // Apply clamped rotations
        cam.transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
    }

    public void AfterGameCam()
    {
        cam.SetActive(false);
        mainCam.SetActive(true);
        gameEnded = true;
    }
}
