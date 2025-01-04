using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeShot : MonoBehaviour
{
    public GameObject ballPrefab; // The projectile prefab
    public Transform spawnPoint; // The position where the bullet spawns
    public float ballSpeed = 10f; // Speed of the bullet
    public int ballCount = 1; // Number of bullets available
    public float cooldownTime = 1.0f; // Cooldown between shots
    public float maxShootDistance = 1000f; // Maximum distance of the raycast
    private float nextFireTime = 0.0f; // Time for the next shot

    void Update()
    {
        // Check if the spacebar is pressed and the cooldown allows firing
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime && ballCount > 0)
        {
            Shoot();
            ballCount--;
            nextFireTime = Time.time + cooldownTime;
        }
    }

    void Shoot()
    {
        // Create the ball instance at the spawn point
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Perform a raycast from the center of the screen
        Camera camera = spawnPoint.GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            // If the ray hits an object, use the hit point as the target
            targetPoint = hit.point;
        }
        else
        {
            // If no object is hit, shoot in the forward direction to a far point
            targetPoint = ray.GetPoint(maxShootDistance);
        }

        // Calculate the direction to the target point
        Vector3 direction = (targetPoint - spawnPoint.position).normalized;

        // Set the velocity and rotation of the bullet
        rb.velocity = direction * ballSpeed;
        ball.transform.rotation = Quaternion.LookRotation(direction);

        Debug.Log("Ball fired towards: " + targetPoint);
    }
}
