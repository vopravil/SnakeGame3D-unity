using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;    public float followSpeed = 5f;    public Vector3 offset = new Vector3(0, 10, 0);
    void LateUpdate()
    {
        if (player == null)
            return;

               Vector3 targetPosition = player.position + offset;

                  transform.position = targetPosition;
    }
}
