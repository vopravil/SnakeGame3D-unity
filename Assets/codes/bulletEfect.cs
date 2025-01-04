using UnityEngine;

public class bulletEffect : MonoBehaviour
{
    public ParticleSystem trailEffect; // Assign the BulletPS particle system in the Inspector

    void Start()
    {
        // Ensure the particle system is assigned
        if (trailEffect == null)
        {
            Debug.LogError("Trail Effect (Particle System) is not assigned in the Inspector!");
            return;
        }

        // Play the particle system at the start
        trailEffect.Play();
        Debug.Log("Particle system started!XXXXXXXXXXXXXXXXX");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (trailEffect != null)
        {
            // Detach the particle system and stop it
            trailEffect.transform.parent = null; // Detach from the bullet
            trailEffect.Stop(); // Stop particle emission
            Debug.Log("Particle system stopped on collision!");

            // Destroy the particle system after it finishes
            Destroy(trailEffect.gameObject, trailEffect.main.duration);
        }

        // Destroy the bullet
       
    }
}
