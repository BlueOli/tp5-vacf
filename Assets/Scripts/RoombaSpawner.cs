using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoombaSpawner : MonoBehaviour
{
    public GameObject roombaPrefab; // Prefab of the friend GameObject
    public GameObject paths; // GameObject containing the seats

    public int numberOfRoombasToSpawn = 3; // Number of friends to spawn

    private Transform[] pathsSpawnPositions; // Array to store the positions of the seats
    private bool[] occupiedPaths; // Array to track whether seats are occupied

    // Start is called before the first frame update
    void Start()
    {
        // Get the positions of all the seats
        pathsSpawnPositions = new Transform[paths.transform.childCount];

        occupiedPaths = new bool[pathsSpawnPositions.Length];
        for (int i = 0; i < paths.transform.childCount; i++)
        {
            pathsSpawnPositions[i] = paths.transform.GetChild(i);
            occupiedPaths[i] = false;
        }

        // Spawn friends and assign them to random unoccupied seats
        SpawnFriends();
    }

    // Spawn friends and assign them to random unoccupied seats
    void SpawnFriends()
    {
        for (int i = 0; i < numberOfRoombasToSpawn; i++)
        {
            // Check if there are unoccupied seats available
            int unoccupiedSeatIndex = GetRandomUnoccupiedSeatIndex();
            if (unoccupiedSeatIndex != -1)
            {
                // Spawn friend at the position of the unoccupied seat

                Renderer renderer = pathsSpawnPositions[unoccupiedSeatIndex].GetComponent<Renderer>();
                Vector3 center = renderer.bounds.center;
                Vector3 extents = renderer.bounds.extents;
                Vector3 spawnPos = center + new Vector3(extents.x, 0, extents.z);

                GameObject newFriend = Instantiate(roombaPrefab, spawnPos, Quaternion.identity);
                occupiedPaths[unoccupiedSeatIndex] = true;
                newFriend.GetComponent<RoombaMovement>().pathBounds = pathsSpawnPositions[unoccupiedSeatIndex];
            }
            else
            {
                Debug.LogWarning("No unoccupied seats available to spawn friend.");
            }
        }
    }

    // Get a random index of an unoccupied seat, returns -1 if all seats are occupied
    int GetRandomUnoccupiedSeatIndex()
    {
        int[] unoccupiedSeatIndices = new int[occupiedPaths.Length];
        int count = 0;
        for (int i = 0; i < occupiedPaths.Length; i++)
        {
            if (!occupiedPaths[i])
            {
                unoccupiedSeatIndices[count] = i;
                count++;
            }
        }

        if (count == 0)
        {
            return -1; // All seats are occupied
        }
        else
        {
            return unoccupiedSeatIndices[Random.Range(0, count)];
        }
    }
}
