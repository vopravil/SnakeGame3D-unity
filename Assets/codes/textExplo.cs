using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textExplo : MonoBehaviour
{
    public ParticleSystem particlePrefab; // The Particle System prefab to instantiate

    void Start()
    {
        ParticleSystem newParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // Play the particle system
        newParticle.Play();

        // Optionally, destroy the particle system after its duration if you want it to clean up automatically
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
            // Instantiate the particle system at the current object's position
           
        }
    }
}
