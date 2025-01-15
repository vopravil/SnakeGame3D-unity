using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossCollisions : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public popUpText PopUpText;
    public ParticleSystem particlePrefab; // The Particle System prefab to instantiate
    private ParticleSystem activeParticle; // To hold the instantiated particle system
    public TMPro.TextMeshProUGUI GreatEnemyFelled;
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBar;
    public float dmg;
    public bool isSlain = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); 

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
                PopUpText.ShowFloatingText(transform.position + new Vector3(0, 5, 10), "-15 hp");
                TakeDamage(dmg); 
                TriggerExplosion(); 
                Destroy(other.gameObject); 
            }
            else if (other.CompareTag("body") || other.CompareTag("tail") || other.CompareTag("head"))
            {
                // Handle collision with snake body or head
                SnakeMovement.Death();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth); // Debugging health value
        if (currentHealth <= 0)
        {
            
            currentHealth = 0;
            isSlain = true;
            Destroy(gameObject);
        };
       


        

        UpdateHealthBar(); // Update health bar
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth /100f;
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
