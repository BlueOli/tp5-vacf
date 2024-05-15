using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public DifficultyManagerSO diffcultySO;
    public int currentDifficulty = 3;
    public RoombaSpawner roombaSpawner;
    public FriendSpawner friendSpawner;

    private void Awake()
    {
        currentDifficulty = diffcultySO.difficulty;
        roombaSpawner.numberOfRoombasToSpawn = currentDifficulty;
        friendSpawner.numberOfFriendsToSpawn = currentDifficulty;
    }    
}
