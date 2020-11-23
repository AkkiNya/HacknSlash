﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnim PlayerAnim;
    private Rigidbody playerBody;
    private Animator anim;
    public float walkSpeed = 100f;
    private float rotationY = -90;
    private float rotationSpeed = 15f;
    [SerializeField]
    private GameObject CollisionBox;
    public bool Moving;
    private bool Running;

    void Awake()
    {
        PlayerAnim = GetComponent<PlayerAnim>();
        playerBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        RotatePlayer();
        CheckCrouch();
        CheckWalk();
        CheckRun();
        CheckDodge();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void DetectMovement()
    {
        if (Input.GetAxisRaw(Axis.verticalaxis) >= 0)
        {   //Require animation to be playing to add force
            //if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
            playerBody.velocity = new Vector3
                (Input.GetAxisRaw(Axis.horizontalaxis) * (walkSpeed * Time.deltaTime),
                 playerBody.velocity.y,
                 playerBody.velocity.z);
            }
            //Increase force by multiplier while run animation is playing
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
            playerBody.velocity = new Vector3
                (Input.GetAxisRaw(Axis.horizontalaxis) * (1.5f * walkSpeed * Time.deltaTime),
                 playerBody.velocity.y,
                 playerBody.velocity.z);
            }
        }
    }
    void RotatePlayer()
    {
        if (Input.GetAxisRaw(Axis.horizontalaxis) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY) * rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetAxisRaw(Axis.horizontalaxis) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY) * rotationSpeed * Time.deltaTime, 0f);
        }
    }
    void CheckCrouch()
    {
        if (Input.GetKeyUp(KeyCode.S) && transform.position.y == 0)
        {
            PlayerAnim.ChangeState(AnimationTags.crouch);
        }
    }
    void CheckWalk()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKeyDown(KeyCode.S)) {
            if (Running){
                PlayerAnim.ChangeState(AnimationTags.run);
            }
            else{
                PlayerAnim.ChangeState(AnimationTags.walk);
            }
            Moving = true;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.S)){
            if (Running){
                PlayerAnim.ChangeState(AnimationTags.run);
            }
            else{
                PlayerAnim.ChangeState(AnimationTags.walk);
            }
            Moving = true;
        }
        else {
            Moving = false;
        }
    }
    void CheckRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Running = false;
        }
    }
    void CheckDodge()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge1")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge2")
            || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            CollisionBox.SetActive(false);
        }
        else
        {
            CollisionBox.SetActive(true);
        }
    }

} // class
