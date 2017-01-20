using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() 
    {
        if (controller.isGrounded) 
        {
            moveDirection = Vector3.right * Input.GetAxis("Horizontal");
            moveDirection = transform.TransformDirection(moveDirection) * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
