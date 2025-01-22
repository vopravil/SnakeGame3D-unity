using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollision : MonoBehaviour
{
    public snakeMovement SnakeMovement;

    void Start()
    {
        if (SnakeMovement == null)
        {
            SnakeMovement = FindObjectOfType<snakeMovement>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") || other.CompareTag("enemy"))
        {
            Debug.Log("Ball collided with wall/enemy");
            FindObjectOfType<audioManager>().Play("BulletCollision");
            Destroy(gameObject);
        }
        else if (other.CompareTag("body") || other.CompareTag("tail"))
        {
            Debug.Log("Ball collided with body/tail");
            Destroy(gameObject);
            FindObjectOfType<audioManager>().Play("BulletCollision");
            if (SnakeMovement != null)
            {
                SnakeMovement.Death();
            }
            else
            {
                Debug.LogError("SnakeMovement reference is missing! Cannot call Death().");
            }
        }
    }
}
