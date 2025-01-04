using System.Collections.Generic;
using UnityEngine;

public class snakeMovement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float defSpeed = 10f;
    public float speedBoost = 5f;
    private Rigidbody rb;
    public List<GameObject> bodyParts = new List<GameObject>();
    public GameObject bodyPrefab;
    public GameObject tailPrefab;
    public int gap = 5;  // Controls the spacing between body parts
    private List<Vector3> positionList = new List<Vector3>();
    private List<Quaternion> rotationList = new List<Quaternion>();
    public bool collided = false;
    public int fruitCount = 0;
    public int kills = 0;
    public TMPro.TextMeshProUGUI youDiedText;
    int rotation = 0;
    public gameManager GameManager;

    public popUpText PopUpText;
    void Start()
    {
        if (GameManager == null)
        {
            GameManager = FindObjectOfType<gameManager>();
        }
        if (PopUpText == null)
        {
            PopUpText = FindObjectOfType<popUpText>();
        }
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < 10; i++)
        {
            GrowBody();
        }
      
        GrowTail();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 90, 0);
            rotation += 90;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, -90, 0);
            rotation -= 90;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            deleteBody();
        }


    }

    void FixedUpdate()
    {
       /* if (Input.GetKeyDown(KeyCode.G))
        {
           
            GrowBody();
            
        }*/
        float speedBoostInput = Input.GetAxis("Vertical");
        moveSpeed = (speedBoostInput != 0f) ? defSpeed + (speedBoost * speedBoostInput) : defSpeed;

        MoveCharacter();
        FollowWithGap();
    }

    void MoveCharacter()
    {
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // Record position and rotation
        positionList.Insert(0, transform.position);
        rotationList.Insert(0, transform.rotation);

        // Limit the history to prevent excessive memory use
        if (positionList.Count > 1000)
        {
            positionList.RemoveAt(positionList.Count - 1);
            rotationList.RemoveAt(rotationList.Count - 1);
        }
    }

    void FollowWithGap()
    {
        double followIndex = 0;
       
        // Set the initial gap offset for each body part based on its index
        for (int i = 0; i < bodyParts.Count; i++)
        {
           
            if (collided is false)
            {

                if (i == bodyParts.Count - 1)
                {
                    if (moveSpeed > defSpeed)
                    {
                        followIndex = Mathf.Clamp((i + 1.4f) * gap +3, 0, positionList.Count - 1);
                    }
                    else
                    {
                        followIndex = Mathf.Clamp((i + 1.4f) * gap + 7, 0, positionList.Count - 1);
                    }
                    
                }
                else
                {
                    followIndex = Mathf.Clamp((i + 1.4f) * gap, 0, positionList.Count - 1);
                }
                bodyParts[i].transform.position = positionList[(int)followIndex];
                bodyParts[i].transform.rotation = rotationList[(int)followIndex];
                
            }
               
                        
                  
                
           
        }
    }

    private void GrowBody()
    {
        GameObject body = Instantiate(bodyPrefab);

        // Set the new body part�s position to match the position of the last body part
        if (bodyParts.Count > 0)
        {
            body.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
            body.transform.rotation = bodyParts[bodyParts.Count - 1].transform.rotation;
        }

        // Insert the new body part at the second-to-last position
        if (bodyParts.Count > 1)
        {
            bodyParts.Insert(bodyParts.Count - 1, body);
        }
        else
        {
            bodyParts.Add(body);
        }
    }

    private void GrowTail()
    {
        GameObject tail = Instantiate(tailPrefab);
        bodyParts.Add(tail);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("head"))
        {
            // Stop the snake if the head collides with the tail or body
            if (other.CompareTag("tail") || other.CompareTag("body") || other.CompareTag("wall") || other.CompareTag("obstacle"))
            {
                Debug.Log("Head collided with tail or body or wall");
                Death();
               
                
            }
            /*else if (other.CompareTag("enemy"))
            {
                if (bodyParts.Count-5 < 2)
                {
                    Death();
                }
                else
                {          
                    Debug.Log("Head collided with enemy");

                    for (int i = 0; i < 5; i++)
                    {
                        deleteBody();
                    }   
                    Destroy(other.gameObject);
                }
            }*/
            // Add a new body part if the head collides with a fruit
            else if (other.CompareTag("fruit"))
            {
                //floatingTextPre.text = "+1";
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 0), "+1F");
                fruitCount++;
               
                Debug.Log("Head collided with fruit");
                GrowBody();  // Adds a new body part
                Destroy(other.gameObject);  // Removes the fruit object from the scene
            }
            else if (other.CompareTag("finish"))
            {
                GameManager.Win();
                
            }

        }
       
    }

    private void deathRoll()
    {
        transform.Rotate(0, 90, 0);
    }

    public void Death()
    {
        GetComponent<BoxCollider>().enabled = false;
        moveSpeed = 0;
        defSpeed = 0;
        collided = true;
        speedBoost = 0;
        youDiedText.gameObject.SetActive(true);
        if (collided == true)
        {
            InvokeRepeating("deathRoll", 0.5f, 0.5f);
        }
    }

    public void deleteBody()
    {
        if (bodyParts.Count > 2)
        {
            GameObject secondLastBodyPart = bodyParts[bodyParts.Count - 2];

            Destroy(secondLastBodyPart);

            bodyParts.RemoveAt(bodyParts.Count - 2);
        }
        else
        {
            Debug.LogWarning("Not enough body parts to remove the second last element.");
        }
    }
   /*  void ShowFloatingText()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 5, 0);
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);

        Instantiate(floatingTextPre, spawnPosition, spawnRotation);
    }
   */

}
