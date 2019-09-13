using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (followTarget != null)       //if we have a follow target (ie attached to the marine/player)
        {                               //make a smooth "follow" by using Lerp to ramp up to the player's changing speed and direction
            transform.position = Vector3.Lerp(this.transform.position, followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
        
    }
}
