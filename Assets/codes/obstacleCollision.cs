using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("obstacle"))
        {
            if (other.CompareTag("ball"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);

            }
        }


    }
}
