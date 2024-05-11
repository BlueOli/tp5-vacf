using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public float crouchSpeed = 2f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        // Get the input from the horizontal and vertical axes (WASD keys or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the player's movement direction
        Vector3 moveDirection = transform.TransformDirection(new Vector3(moveHorizontal, 0f, moveVertical));

        // Apply movement to the player
        rb.velocity = moveDirection * speed;
    }
}

