using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendFollow : MonoBehaviour
{
    public Transform player;
    public Transform target; // Reference to the player's transform
    public bool canFollow = false;


    public float speed = 5f; // Speed at which the friend follows the player
    public float runSpeed = 8f;
    public float crouchSpeed = 2f;
    public float walkSpeed = 5f;

    public float leavingThreshold = 3f;
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
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
                rb.velocity = direction * speed;
                if (distance > leavingThreshold)
                {
                    LeaveTarget();
                    LeaveFriend();
                }
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

    public void LeaveTarget()
    {
        if(target != null)
        {
            if(target == player)
            {
                player.GetComponent<PlayerInteraction>().friendFollowing = null;
            }
            else
            {
                target.GetComponent<FriendFollow>().friendFollowing = null;
            }
            target = null;
            canFollow = false;
        }

        player.GetComponent<PlayerInteraction>().FriendCount();
    }

    public void LeaveFriend()
    {
        if(friendFollowing != null)
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

        player.GetComponent<PlayerInteraction>().FriendCount();
    }

    public bool IsFriendInList(GameObject friend)
    {
        bool isInList = false;

        if (friendFollowing != null)
        {
            if (friend == friendFollowing)
            {
                isInList = true;
            }
            else
            {
                isInList = friendFollowing.GetComponent<FriendFollow>().IsFriendInList(friend);
            }
        }

        return isInList;
    }
}
