using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSpawner : MonoBehaviour
{
    public GameObject friendPrefab; // Prefab of the friend GameObject
    public GameObject seats; // GameObject containing the seats

    public int numberOfFriendsToSpawn = 3; // Number of friends to spawn

    private Transform[] seatPositions; // Array to store the positions of the seats
    private bool[] occupiedSeats; // Array to track whether seats are occupied

    // Start is called before the first frame update
    void Start()
    {
        // Get the positions of all the seats
        seatPositions = new Transform[seats.transform.childCount];
        occupiedSeats = new bool[seatPositions.Length];
        for (int i = 0; i < seats.transform.childCount; i++)
        {
            seatPositions[i] = seats.transform.GetChild(i);
            occupiedSeats[i] = false;
        }

        // Spawn friends and assign them to random unoccupied seats
        SpawnFriends();
    }

    // Spawn friends and assign them to random unoccupied seats
    void SpawnFriends()
    {
        for (int i = 0; i < numberOfFriendsToSpawn; i++)
        {
            // Check if there are unoccupied seats available
            int unoccupiedSeatIndex = GetRandomUnoccupiedSeatIndex();
            if (unoccupiedSeatIndex != -1)
            {
                // Spawn friend at the position of the unoccupied seat
                GameObject newFriend = Instantiate(friendPrefab, seatPositions[unoccupiedSeatIndex].position, Quaternion.identity);
                occupiedSeats[unoccupiedSeatIndex] = true;
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
        int[] unoccupiedSeatIndices = new int[occupiedSeats.Length];
        int count = 0;
        for (int i = 0; i < occupiedSeats.Length; i++)
        {
            if (!occupiedSeats[i])
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