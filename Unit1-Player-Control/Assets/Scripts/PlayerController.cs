using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float accel = Input.GetAxis("Vertical");

        if (accel != 0)
        {
            transform.Translate(0, 0, accel * speed);
        }
    }
}
