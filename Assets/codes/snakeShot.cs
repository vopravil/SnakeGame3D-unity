using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeShot : MonoBehaviour
{
    public GameObject ballPrefab;    public Transform spawnPoint;    public float ballSpeed = 10f;    public int ballCount = 1;    public float cooldownTime = 1.0f;    public float maxShootDistance = 1000f;    private float nextFireTime = 0.0f;
    void Update()
    {
               if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime && ballCount > 0)
        {
            Shoot();
            ballCount--;
            nextFireTime = Time.time + cooldownTime;
        }
    }

    void Shoot()
    {
               GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

               Camera camera = spawnPoint.GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
                       targetPoint = hit.point;
        }
        else
        {
                       targetPoint = ray.GetPoint(maxShootDistance);
        }

               Vector3 direction = (targetPoint - spawnPoint.position).normalized;

               rb.velocity = direction * ballSpeed;
        ball.transform.rotation = Quaternion.LookRotation(direction);

        Debug.Log("Ball fired towards: " + targetPoint);
    }
}
