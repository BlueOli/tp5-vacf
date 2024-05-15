using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public Transform pathBounds; // Reference to the GameObject defining the path boundaries

    private Vector3[] pathPoints; // Array to store the path boundary points
    private int currentWaypointIndex = 0; // Index of the current waypoint in the path
    private bool movingForward = true; // Flag to track movement direction

    private int pathType = 0;
    public bool isPathTypeRandom = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isPathTypeRandom)
        {
            pathType = Random.Range(0, 4);
        }
       
        // Get the path boundary points from the pathBounds GameObject
        if (pathBounds != null)
        {
            pathPoints = GetPathPoints(pathBounds);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the Roomba along the path
        MoveRoomba();
    }

    // Move the Roomba along the path
    void MoveRoomba()
    {
        if (pathPoints.Length == 0)
        {
            Debug.LogWarning("No path points defined for Roomba movement.");
            return;
        }

        // Calculate the direction towards the current waypoint
        Vector3 direction = pathPoints[currentWaypointIndex] - transform.position;

        // Check if the Roomba has reached the current waypoint
        if (direction.magnitude < 0.1f)
        {
            // Move to the next waypoint in the path
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= pathPoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
        }

        // Normalize the direction vector and move the Roomba
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // Get the path boundary points from the pathBounds GameObject
    Vector3[] GetPathPoints(Transform bounds)
    {
        Vector3[] points = new Vector3[4];
        Renderer renderer = bounds.GetComponent<Renderer>();

        // Get the bounds of the pathBounds GameObject
        Vector3 center = renderer.bounds.center;
        Vector3 extents = renderer.bounds.extents;


        switch (pathType)
        {
            case 0:
                points[0] = center + new Vector3(extents.x, 0, extents.z);
                points[3] = center + new Vector3(-extents.x, 0, extents.z);
                points[1] = center + new Vector3(extents.x, 0, -extents.z);
                points[2] = center + new Vector3(-extents.x, 0, -extents.z);
                break;
            case 1:
                points[0] = center + new Vector3(extents.x, 0, extents.z);
                points[1] = center + new Vector3(-extents.x, 0, extents.z);
                points[3] = center + new Vector3(extents.x, 0, -extents.z);
                points[2] = center + new Vector3(-extents.x, 0, -extents.z);
                break;
            case 2:
                points[0] = center + new Vector3(extents.x, 0, extents.z);
                points[2] = center + new Vector3(-extents.x, 0, extents.z);
                points[1] = center + new Vector3(extents.x, 0, -extents.z);
                points[3] = center + new Vector3(-extents.x, 0, -extents.z);
                break;
            case 3:
                points[0] = center + new Vector3(extents.x, 0, extents.z);
                points[1] = center + new Vector3(-extents.x, 0, extents.z);
                points[2] = center + new Vector3(extents.x, 0, -extents.z);
                points[3] = center + new Vector3(-extents.x, 0, -extents.z);
                break;
        }


        

        return points;
    }
}
