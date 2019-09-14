using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))                                                   //while the player is holding the left(0) mouse button
        {
            if (!IsInvoking("fireBullet"))                                                  //If we have not set up (invoked) our bullet firing yet
            {
                InvokeRepeating("fireBullet", 0.0f, 0.1f);                                  //set our fire method to continously fire (every 0.1s)
            }                                                                               //(overhead? I guess this is more framerate independant than a simple countdown)
        }
        else if (Input.GetMouseButtonUp(0))                                                 //once the left(0) button is release, cancel the firing
        {                                                                                   //(will this continue to be called while the player is not doing anything?)
            CancelInvoke("fireBullet");
        }
    }

    void fireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;                        //create a bullet from our prefab

        bullet.transform.position = launchPosition.position;                                //place it at the launcher (gun)

        bullet.GetComponent<Rigidbody>().velocity = this.transform.parent.forward * 100;    //set it's velocity to be in the direction the gun is pointing to

    }
}
