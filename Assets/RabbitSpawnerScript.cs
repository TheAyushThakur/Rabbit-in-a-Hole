using UnityEngine;

public class RabbitSpawnerScript : MonoBehaviour
{
    public GameObject rabbitPrefab; // The rabbit prefab
    public Transform[] holePositions; // Array to hold hole markers
    public float showTime = 0.6f; // Time the rabbit stays visible (0.7 seconds)

    private GameObject currentRabbit; // Tracks the active rabbit
    private int lastHoleIndex = -1; // Tracks the index of the last hole

    void Start()
    {
        // Start spawning rabbits at regular intervals
        InvokeRepeating(nameof(SpawnRabbit), 1f, 0.6f); // Spawn every 0.7 seconds
    }

    void SpawnRabbit()
    {
        if (currentRabbit != null) return; // Only one rabbit should appear at a time

        // Select a random hole, ensuring it’s not the same as the last hole
        int randomIndex = Random.Range(0, holePositions.Length);

        // Ensure the random index is not the same as the last one
        while (randomIndex == lastHoleIndex)
        {
            randomIndex = Random.Range(0, holePositions.Length);
        }

        // Update the last hole index to the current one
        lastHoleIndex = randomIndex;

        Vector3 spawnPosition = holePositions[randomIndex].position;

        // Instantiate the rabbit prefab at the selected hole
        currentRabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"Rabbit spawning at position: {spawnPosition}");

        // Schedule the rabbit to disappear after 'showTime'
        Invoke(nameof(DespawnRabbit), showTime);
    }

    void DespawnRabbit()
    {
        if (currentRabbit != null)
        {
            Destroy(currentRabbit); // Remove the rabbit from the scene
            currentRabbit = null; // Reset the reference
        }
    }
}
