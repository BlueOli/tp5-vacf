using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 1f; // Radius to check for friend GameObjects
    public GameObject friendFollowing = null;
    public int friendsTagged = 0;

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

                bool isFriendInList = false;

                if(friendFollowing != null)
                {
                    if(friendFollowing == obj)
                    {
                        isFriendInList = true;
                    }
                    else
                    {
                        isFriendInList = friendFollowing.GetComponent<FriendFollow>().IsFriendInList(obj);
                    }
                }

                if (obj.CompareTag("Friend") && !isFriendInList)
                {
                    GameObject friend = obj;
                    // Debug the name of the friend GameObject
                    Debug.Log("Found friend: " + friend.name);
                    FriendFollow friendFollow = friend.GetComponent<FriendFollow>();
                    if(friendFollow != null)
                    {
                        if(friendFollowing == null)
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

                    FriendCount();
                }               
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(friendFollowing != null)
            {
                FriendFollow friendFollow = friendFollowing.GetComponent<FriendFollow>();
                if (friendFollow != null)
                {
                    friendsTagged = 0;
                    friendFollow.target = null;
                    friendFollow.canFollow = false;
                    friendFollow.LeaveFriend();
                    friendFollowing = null;
                }
            }

            FriendCount();
        }
    }

    // Draw gizmos to visualize the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public void FriendCount()
    {
        friendsTagged = 0;

        if(friendFollowing != null)
        {
            bool endReached = false;
            GameObject lastFriend = friendFollowing;
            do
            {
                friendsTagged++;
                GameObject newFriend = lastFriend.GetComponent<FriendFollow>().friendFollowing;
                if(newFriend != null)
                {
                    endReached = false;
                    lastFriend = newFriend;
                }
                else
                {
                    endReached = true;
                }

            } while (!endReached);
        }
    }
}
