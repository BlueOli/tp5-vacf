using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int friendAmount = other.gameObject.GetComponent<PlayerInteraction>().friendsTagged;
            Debug.Log("Te escapaste con " + friendAmount + " amigos.");
        }
    }
}
