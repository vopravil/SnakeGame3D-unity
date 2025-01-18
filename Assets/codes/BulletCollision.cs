using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public snakeShot SnakeShot;
    public popUpText PopUpText;
   
    void Start()
    {
        if (PopUpText == null)
        {
            PopUpText = FindObjectOfType<popUpText>();
        }
        if (SnakeShot == null)
        {
            SnakeShot = FindObjectOfType<snakeShot>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("bullet"))
        {
            if (other.CompareTag("head"))
            {
                FindObjectOfType<audioManager>().Play("BulletPicked");
                GetComponent<SphereCollider>().enabled = false;
                Destroy(gameObject);
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 0),"+1B");
                Debug.Log("bullet picked");
                SnakeShot.ballCount++;
              

            }
        }
        
        
    }

    
}
