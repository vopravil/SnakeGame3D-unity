using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshPro

public class enemyCollision : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public popUpText PopUpText;
    public ParticleSystem particlePrefab; // The Particle System prefab to instantiate
    private ParticleSystem activeParticle; // To hold the instantiated particle system

    void Start()
    {
        if (PopUpText == null)
        {
            PopUpText = FindObjectOfType<popUpText>();
        }

        if (SnakeMovement == null)
        {
            SnakeMovement = FindObjectOfType<snakeMovement>();
        }

        if (particlePrefab == null)
        {
            Debug.LogError("Particle prefab is not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("enemy"))
        {
            if (other.CompareTag("ball"))
            {
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 0), "Dead!");
                TriggerExplosion(); // Only trigger the particle system when hit by the ball
                Destroy(gameObject);
                Destroy(other.gameObject);
                SnakeMovement.kills++;
            }
            else if (other.CompareTag("body") || other.CompareTag("tail") || other.CompareTag("head"))
            {
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 0), "-5hp");
                TriggerExplosion(); // Only trigger the particle system when hit by the snake body
                Destroy(gameObject);

                if (SnakeMovement.bodyParts.Count - 5 < 2)
                {
                    SnakeMovement.Death();
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        SnakeMovement.deleteBody();
                    }
                }
            }
        }
    }

    public void TriggerExplosion()
    {
        if (particlePrefab != null)
        {
            // Instantiate the particle system at the current object's position
            activeParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // Play the particle system
            activeParticle.Play();

            // Optionally, destroy the particle system after its duration if you want it to clean up automatically
            Destroy(activeParticle.gameObject, activeParticle.main.duration);

            Debug.Log("Particle system triggered!");
        }
    }
}
