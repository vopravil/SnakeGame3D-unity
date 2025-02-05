using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class enemyCollision : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public popUpText PopUpText;
    public ParticleSystem particlePrefab;    private ParticleSystem activeParticle;
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
                TriggerExplosion();                Destroy(gameObject);
                Destroy(other.gameObject);
                SnakeMovement.kills++;
            }
            else if (other.CompareTag("body") || other.CompareTag("tail") || other.CompareTag("head"))
            {
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 0), "-5hp");
                TriggerExplosion();                Destroy(gameObject);

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
                       activeParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

                       activeParticle.Play();

                       Destroy(activeParticle.gameObject, activeParticle.main.duration);

            Debug.Log("Particle system triggered!");
        }
    }
}
