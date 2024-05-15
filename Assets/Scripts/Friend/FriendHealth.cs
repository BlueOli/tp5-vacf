using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FriendHealth : MonoBehaviour
{
    public int health = 1;
    public FriendFollow friendFollow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roomba"))
        {
            if (health > 0)
            {
                health--;
            }
        }
    }

    public void Update()
    {
        if (health <= 0)
        {
            friendFollow.LeaveTarget();
            friendFollow.LeaveFriend();
            GameObject.Destroy(this.gameObject);
        }
    }
}
