using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingTextTime : MonoBehaviour
{
    public float destroyTime = 1;

       void Start()
    {
        Destroy(gameObject, destroyTime);
    }

       void Update()
    {
        
    }
}
