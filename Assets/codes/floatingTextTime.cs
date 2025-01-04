using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingTextTime : MonoBehaviour
{
    public float destroyTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
