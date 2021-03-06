﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioEvent = FMODUnity.StudioEventEmitter;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 1.5f;

    private bool onGround = false;

    private int numGroundObjectsInContact = 0;
    private float lastAccel = 0.0f;

    private AudioEvent engineAudio; 

    // Start is called before the first frame update
    void Start()
    {
        engineAudio = GetComponent<AudioEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            float accel = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            if (accel == 0)
            {
                turn = 0;
            }

            //Invert turning direction if going backwards
            else if (accel < 0)
            {
                turn *= -1;
            }

            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
            transform.Translate(0, 0, Time.deltaTime * speed * accel);

            for (int i = 0; i < 2; i++)
            {
                transform.GetChild(i).Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
            }

            accel = System.Math.Abs(accel);

            //If we're decellerating
            if (accel < lastAccel)
            {
                engineAudio.SetParameter("speed", 0.0f);
            }
            else
            {
                engineAudio.SetParameter("speed", accel);
            }

            lastAccel = accel;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ++numGroundObjectsInContact;
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (--numGroundObjectsInContact == 0)
            {
                onGround = false;
            }
        }
    }
}
