using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 1.5f;

    private bool onGround = false;
    private int numGroundObjectsInContact = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            float accel = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            if (accel != 0)
            {
                transform.Rotate(0, turn * turnSpeed, 0);
            }

            transform.Translate(0, 0, Time.deltaTime * speed * accel);


            for (int i = 0; i < 2; i++)
            {
                //Wheel spin
                transform.GetChild(i).Rotate(0, turnSpeed * turn * Time.deltaTime, 0);
            }
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
