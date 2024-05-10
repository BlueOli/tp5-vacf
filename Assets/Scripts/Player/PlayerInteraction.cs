using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 1f; // Radius to check for friend GameObjects

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get all colliders within the interaction radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);

            // Check each collider
            foreach (Collider collider in hitColliders)
            {
                // Get the GameObject attached to the collider
                GameObject obj = collider.gameObject;

                if (obj.CompareTag("Friend"))
                {
                    GameObject friend = obj;
                    // Debug the name of the friend GameObject
                    Debug.Log("Found friend: " + friend.name);
                }               
            }
        }
    }

    // Draw gizmos to visualize the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
