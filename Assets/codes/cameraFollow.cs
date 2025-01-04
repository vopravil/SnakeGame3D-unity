using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float followSpeed = 5f; // Speed at which the camera follows
    public Vector3 offset = new Vector3(0, 10, 0); // Offset from the player's position

    void LateUpdate()
    {
        if (player == null)
            return;

        // Target position with the offset
        Vector3 targetPosition = player.position + offset;

        // Smoothly interpolate between the current position and the target position
      //  transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
      transform.position = targetPosition;
    }
}
