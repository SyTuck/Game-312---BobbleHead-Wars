﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Alien : MonoBehaviour
{

    public Transform target;
    public float navigationUpdate;

    private NavMeshAgent agent;
    private float navigationTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0.0f;
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }
}
