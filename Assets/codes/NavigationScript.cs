using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find the player automatically by tag
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

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.destination = player.position;
        }
    }
}
