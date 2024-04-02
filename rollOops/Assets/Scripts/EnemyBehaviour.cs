using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 2f; // Speed at which the enemy moves

    // Update is called once per frame
    void Update()
    {
        if (player != null) // Ensure player is not null
        {
            Vector3 directionToPlayer = player.position - transform.position; // Calculate direction to player
            transform.Translate(GetNextMoveDirection(directionToPlayer) * moveSpeed * Time.deltaTime); // Move towards player
        }
    }

    // Greedy algorithm to determine next move direction
    private Vector3 GetNextMoveDirection(Vector3 directionToPlayer)
    {
        // Find the axis with the largest absolute value in the direction vector
        float maxAxisValue = Mathf.Max(Mathf.Abs(directionToPlayer.x), Mathf.Abs(directionToPlayer.y), Mathf.Abs(directionToPlayer.z));

        // Create a vector where only the axis with the largest absolute value is non-zero
        Vector3 greedyDirection = new Vector3(
            directionToPlayer.x / maxAxisValue,
            directionToPlayer.y / maxAxisValue,
            directionToPlayer.z / maxAxisValue
        );

        return greedyDirection.normalized; // Return normalized greedy direction
    }
}
