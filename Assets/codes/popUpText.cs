using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class popUpText : MonoBehaviour
{     public TextMeshPro floatingTextPre;
    

    void Start()
    {
        
    }

       void Update()
    {

    }
    public void ShowFloatingText(Vector3 spawnPosition, string message)
    {
        floatingTextPre.text = message;
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);

        Instantiate(floatingTextPre, spawnPosition, spawnRotation);
    }
}
