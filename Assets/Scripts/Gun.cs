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
        
    }

    void fireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;                        //create a bullet from our prefab

        bullet.transform.position = launchPosition.position;                                //place it at the launcher (gun)

        bullet.GetComponent<Rigidbody>().velocity = this.transform.parent.forward * 100;    //set it's velocity to be in the direction the gun is pointing to

    }
}
