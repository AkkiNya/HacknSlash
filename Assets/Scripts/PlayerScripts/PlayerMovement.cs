﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnim playerAnim;
    private Rigidbody playerBody;

    public float walkSpeed = 100f;

    private float rotationY = -90;
    private float rotationSpeed = 15f;

    void Awake()
    {
        playerAnim = GetComponent<PlayerAnim>();
        playerBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        RotatePlayer();
        AnimatePlayerWalk();
        AnimateCrouch();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void DetectMovement()
    {
        if (Input.GetAxisRaw(Axis.verticalaxis) >= 0)
        {
            playerBody.velocity = new Vector3
            (Input.GetAxisRaw(Axis.horizontalaxis) * (walkSpeed * Time.deltaTime),
            playerBody.velocity.y,
            playerBody.velocity.z);
        }
    }
    void RotatePlayer()
    {
        if(Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY) * rotationSpeed, 0f);
        }
        else if(Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY) * rotationSpeed, 0f);
        }
    }
    void AnimatePlayerWalk()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKeyDown(KeyCode.S)) {
            playerAnim.Walk(true);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.S)){
            playerAnim.Walk(true);
        }
        else {
            playerAnim.Walk(false);
        }
    }
    void AnimateCrouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            playerAnim.Crouch(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
            playerAnim.Crouch(false);
    }

} // class
