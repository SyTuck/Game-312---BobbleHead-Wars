using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject player;
    public Transform elevator;

    private Animator arenaAnimator;
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        arenaAnimator = this.GetComponent<Animator>();
        sphereCollider = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sphereCollider.enabled)                                                                     //because of an issue with collisions getting passed up from the children
        {                                                                                               //I'm blocking this code until the sphere collider is ready to be used
            GameObject fngWall = GameObject.Find("WallTrigger");                                        //and disable the wall trigger otherwise it immediately triggers and activates this code
            fngWall.SetActive(false);                                                                   //once the sphere collider is enabled at the end of the game


//            GameObject funMarine = GameObject.Find("SpaceMarine");                                    //some fun with ragdolling the marine but it is becoming too tricky to continue
//            Rigidbody[] rbs = funMarine.gameObject.GetComponentsInChildren<Rigidbody>();
//            foreach (Rigidbody rb in rbs)
//            {
//                rb.isKinematic = false;
//                rb.useGravity = true;
//            }

//            if ((other.transform.position.x < 2.0f) && (other.transform.position.x > -2.0f)
//             && (other.transform.position.z < 2.0f) && (other.transform.position.z > -2.0f))
//                {
                Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
                player.transform.parent = elevator.transform;
                player.GetComponent<PlayerController>().enabled = false;

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
                arenaAnimator.SetBool("OnElevator", true);
//                }
        }
    }

    public void ActivatePlatform()
    {
        sphereCollider.enabled = true;
    }
}
