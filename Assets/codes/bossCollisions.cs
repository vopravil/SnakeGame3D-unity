using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossCollisions : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public popUpText PopUpText;
    public ParticleSystem particlePrefab;    private ParticleSystem activeParticle;    public TMPro.TextMeshProUGUI GreatEnemyFelled;
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
                               SnakeMovement.Death();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);        if (currentHealth <= 0)
        {
            
            currentHealth = 0;
            isSlain = true;
            Destroy(gameObject);
        };
       


        

        UpdateHealthBar();    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth /100f;
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
