using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.FindWithTag("head");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Ensure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.destination = player.position;
        }
    }
}
