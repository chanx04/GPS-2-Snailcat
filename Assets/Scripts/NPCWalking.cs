using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    public List<Transform> walkingPoints;  // List of points the NPC will walk to
    public float walkSpeed = 3f;         // Speed of the NPC
    public bool canWalk = true;            // Condition to start/stop walking

    private int currentPointIndex = 0;     // The current point the NPC is walking to
    private NavMeshAgent navMeshAgent;     // Reference to the NavMeshAgent for movement

    public NPCMinigame npcMinigame;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkSpeed;

        if (walkingPoints.Count > 0)
        {
            navMeshAgent.destination = walkingPoints[currentPointIndex].position;  // Start walking to the first point
        }
    }

    void Update()
    {
        if (npcMinigame.minigameOn == false && canWalk && walkingPoints.Count > 0)
        {
            // Check if the NPC has reached its current destination
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                GoToNextPoint();
            }
        }
        else if (npcMinigame.minigameOn == true)
        {
            //navMeshAgent.isStopped = true; // Stop movement when canWalk is false
            StopWalking();
        }
        else
        {
            ResumeWalking();
        }
    }

    // Move to the next point in the list
    void GoToNextPoint()
    {
        if (walkingPoints.Count == 0) return;

        // Increment the point index, loop back to the first point if at the end
        currentPointIndex = (currentPointIndex + 1) % walkingPoints.Count;

        // Set the next destination
        navMeshAgent.destination = walkingPoints[currentPointIndex].position;
    }

    // Call this method to stop walking
    public void StopWalking()
    {
        canWalk = false;
        navMeshAgent.isStopped = true;
    }

    // Call this method to resume walking
    public void ResumeWalking()
    {
        canWalk = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = walkingPoints[currentPointIndex].position;  // Resume walking to the next point
    }
}
