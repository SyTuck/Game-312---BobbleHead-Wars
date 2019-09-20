﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;
    public Animator bodyAnimator;

    private Vector3 currentLookTarget = Vector3.zero;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

/*
        Vector3 pos = this.transform.position;                              //modify the player's current position
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;  //by the value/directions of the input manager's key presses
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;    //(horizontal and vertical are tags for the arrow and WASD keys)
        this.transform.position = pos;                                      //(deltaTime is to make a frame rate independent scale for the speed per update loop)
   */
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
            bodyAnimator.SetBool("IsMoving", true);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.green);
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
            Vector3 targetPosition = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

            Quaternion rotation = Quaternion.LookRotation(targetPosition - this.transform.position);

            this.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
        }
    }
}
