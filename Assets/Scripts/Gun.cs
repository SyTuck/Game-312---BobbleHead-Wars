using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    public bool isUpgraded;
    public float upgradeTime = 10.0f;

    public bool isDead = false;

    private float currentTime;


    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource> ();
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

        if (isUpgraded)
        {
            currentTime += Time.deltaTime;
            if (currentTime > upgradeTime)
            {
                isUpgraded = false;
            }
        }
    }

    void fireBullet()
    {


        if (isDead)                                                         //cancel the Invoke otherwise we continue to spawn bullets if firing upon death
        {                                                                   //this doesn't actually stop the Repeated Invoke, but wrapping the spawn
            CancelInvoke("fireBullet");                                     //in an "if" stops the bullets from continously spawning
        }
        else
        {
            Rigidbody bullet = createBullet();
            bullet.velocity = this.transform.parent.forward * 100.0f;    //set it's velocity to be in the direction the gun is pointing to

            if (isUpgraded)
            {
                Rigidbody bullet2 = createBullet();
                bullet2.velocity = (this.transform.right + this.transform.parent.forward / 0.33f) * 33.0f;    //set it's velocity to be in the direction the gun is pointing to

                Rigidbody bullet3 = createBullet();
                bullet3.velocity = ((this.transform.right * -1.0f) + this.transform.parent.forward / 0.33f) * 33.0f;    //set it's velocity to be in the direction the gun is pointing to
                audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
            }
            else
            {
                audioSource.PlayOneShot(SoundManager.Instance.gunFire);
            }
        }
    }

    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;                        //create a bullet from our prefab
        bullet.transform.position = launchPosition.position;                                //place it at the launcher (gun)\
        return bullet.GetComponent<Rigidbody>();
    }

    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }
}
