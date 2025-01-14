using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossNavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Vector3 startingPoint; // Store the boss's starting position
    public float moveRadius = 20f; // Initial maximum movement radius
    public float radiusIncrement = 15f; // Amount to increase the radius
    public float incrementInterval = 30f; // Time interval for radius increment (in seconds)
    private float timeSinceLastIncrement; // Timer to track time
    public GameObject enemyPrefab;
    public GameObject[] terrains; // Array of terrains
    public GameObject[] spawnpoints;
    private int currentTerrainIndex = 0; // Tracks the active terrain

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position; // Save the initial position as the center of the allowed range
        timeSinceLastIncrement = 0f; // Initialize the timer

        // Ensure only the first terrain is active at the start
        ActivateTerrain(0);
        Instantiate(enemyPrefab, spawnpoints[0].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timeSinceLastIncrement += Time.deltaTime;

        // Check if it's time to increase the radius
        if (timeSinceLastIncrement >= incrementInterval)
        {
            moveRadius += radiusIncrement; // Increase the movement radius
            timeSinceLastIncrement = 0f; // Reset the timer
           
            // Switch to the next terrain if available
            if (currentTerrainIndex < terrains.Length - 1)
            {
                
                currentTerrainIndex++;
                for (int i = 0; i <= currentTerrainIndex; i++)
                {
                    Instantiate(enemyPrefab, spawnpoints[i].transform.position, Quaternion.identity);
                }
                ActivateTerrain(currentTerrainIndex);
            }
        }

        Vector3 playerPosition = player.position;

        // Calculate the direction and distance to the player
        Vector3 directionToPlayer = playerPosition - startingPoint;
        if (directionToPlayer.magnitude > moveRadius)
        {
            // Clamp the player's position to stay within the move radius
            playerPosition = startingPoint + directionToPlayer.normalized * moveRadius;
        }

        // Set the clamped destination
        agent.destination = playerPosition;
    }

    // Activates the specified terrain and disables others
    private void ActivateTerrain(int index)
    {
        for (int i = 0; i < terrains.Length; i++)
        {
            terrains[i].SetActive(i == index);
        }
    }
}
