using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCamera : MonoBehaviour
{
       public GameObject cam;
    public GameObject mainCam;

       public float horizontalSensitivity = 50f;
    public float verticalSensitivity = 50f;
    private int defCamYPosition = 0;

       public float maxVerticalAngle = 20f;    public float minVerticalAngle = -20f;    public float maxHorizontalAngle = 30f;    public float minHorizontalAngle = -30f;
    private bool gunCamView = false;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    private bool gameEnded = false;

    void Start()
    {
               cam.SetActive(false);
        mainCam.SetActive(true);
    }

    void Update()
    {
               if (Input.GetKeyDown(KeyCode.Mouse1) && gameEnded == false)
        {
            gunCamView = !gunCamView;
            if (!gunCamView)
            {
                               horizontalRotation = 0f;
                verticalRotation = 0f;
                cam.transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
            }

            cam.SetActive(gunCamView);
            mainCam.SetActive(!gunCamView);
        }



               if (gunCamView)
        {
            HandleGunCameraRotation();
        }
    }

    void HandleGunCameraRotation()
    {
               float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

               horizontalRotation += mouseX;
        horizontalRotation = Mathf.Clamp(horizontalRotation, minHorizontalAngle, maxHorizontalAngle);

               verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

               cam.transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
    }

    public void AfterGameCam()
    {
        cam.SetActive(false);
        mainCam.SetActive(true);
        gameEnded = true;
    }
}
