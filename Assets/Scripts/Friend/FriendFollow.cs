using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendFollow : MonoBehaviour
{
    public Transform player;
    public Transform target; // Reference to the player's transform
    public bool canFollow = false;
    public float followSpeed = 5f; // Speed at which the friend follows the player
    public float stoppingDistance = 1.1f; // Distance at which the friend stops following
    public GameObject friendFollowing = null;

    private Rigidbody rb; // Reference to the Rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the Rigidbody component attached to the friend
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if the player's transform is assigned
        if (target != null && canFollow)
        {
            // Calculate the direction from the friend to the player
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Normalize the direction vector
            direction.Normalize();

            // If the friend is not within stopping distance, move towards the player
            if (distance > stoppingDistance && distanceToPlayer > stoppingDistance)
            {
                // Apply force to move the friend towards the player
                rb.velocity = direction * followSpeed;
            }
            else
            {
                // If within stopping distance, stop moving
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void AddFriend(GameObject friend)
    {
        FriendFollow friendFollow = friend.GetComponent<FriendFollow>();
        if (friendFollow != null)
        {
            if (friendFollowing == null)
            {
                friendFollow.target = this.transform;
                friendFollowing = friend;
                friendFollow.canFollow = true;
            }
            else
            {
                friendFollowing.GetComponent<FriendFollow>().AddFriend(friend);
            }
        }
    }

    public void LeaveFriend()
    {
        FriendFollow friendFollow = friendFollowing.GetComponent<FriendFollow>();
        if (friendFollow != null)
        {
            friendFollow.target = null;
            friendFollow.canFollow = false;
            friendFollow.LeaveFriend();
            friendFollowing = null;
        }
    }
}
