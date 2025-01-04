using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAndDownMovement2 : MonoBehaviour
{
    // Parameters for levitation
    [Header("Levitation Settings")]
    public float levitationHeight = 0.5f; // Height of levitation
    public float levitationSpeed = 2f;   // Speed of levitation

    // Parameters for rotation
    [Header("Rotation Settings")]
    public float rotationSpeed = 50f;   // Speed of rotation (degrees per second)

    // Starting position
    private Vector3 startPosition;

    void Start()
    {
        // Save the starting position of the fruit
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position for levitation
        float newY = startPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;

        // Apply the levitation movement
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Apply rotation around the Y-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
