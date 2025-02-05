using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossNavigationScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Vector3 startingPoint;    public float moveRadius = 20f;    public float radiusIncrement = 15f;    public float incrementInterval = 30f;    private float timeSinceLastIncrement;    public GameObject enemyPrefab;
    public GameObject[] terrains;    public GameObject[] spawnpoints;
    private int currentTerrainIndex = 0;
       void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position;        timeSinceLastIncrement = 0f;
               ActivateTerrain(0);
        Instantiate(enemyPrefab, spawnpoints[0].transform.position, Quaternion.identity);
    }

       void Update()
    {
               timeSinceLastIncrement += Time.deltaTime;

               if (timeSinceLastIncrement >= incrementInterval)
        {
            moveRadius += radiusIncrement;            timeSinceLastIncrement = 0f;            FindObjectOfType<audioManager>().Play("BossLaugh");
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

               Vector3 directionToPlayer = playerPosition - startingPoint;
        if (directionToPlayer.magnitude > moveRadius)
        {
                       playerPosition = startingPoint + directionToPlayer.normalized * moveRadius;
        }

               agent.destination = playerPosition;
    }

       private void ActivateTerrain(int index)
    {
        for (int i = 0; i < terrains.Length; i++)
        {
            terrains[i].SetActive(i == index);
        }
    }
}
