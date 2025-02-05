using UnityEngine;

public class upAndDownMovement : MonoBehaviour
{
       [Header("Levitation Settings")]
    public float levitationHeight = 0.5f;    public float levitationSpeed = 2f;  
       [Header("Rotation Settings")]
    public float rotationSpeed = 50f;  
       private Vector3 startPosition;

    void Start()
    {
               startPosition = transform.position;
    }

    void Update()
    {
               float newY = startPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;

               transform.position = new Vector3(startPosition.x, newY, startPosition.z);

               transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
