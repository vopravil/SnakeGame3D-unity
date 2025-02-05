using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textExplo : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    void Start()
    {
        ParticleSystem newParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

               newParticle.Play();

               Destroy(newParticle.gameObject, newParticle.main.duration);

        Debug.Log("Particle system triggered!");
        if (particlePrefab == null)
        {
            Debug.LogError("Particle prefab is not assigned in the Inspector!");
        }
    }


    public void TriggerExplosion()
    {
        if (particlePrefab != null)
        {
                      
        }
    }
}
