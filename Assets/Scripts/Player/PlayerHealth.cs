using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roomba"))
        {
            if(health > 0)
            {
                health--;
            }
        }
    }
}
