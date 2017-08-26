using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spaceshipScript : MonoBehaviour
{
    public int Speed = 10;
    public GameObject bullet;
    
    // EXTRA
    public float LeftConstraint;
    public float RightConstraint;
    public float TopConstraint;
    public float BottomConstraint;
    public float Buffer;
    private float distanceZ = 1.0f;

    void Awake()
    {
        //http://answers.unity3d.com/questions/276836/fall-off-left-side-of-screen-and-spawn-on-right.html
        // set Vector3 to ( camera left/right limits, spaceship Y, spaceship Z )
        // this will find a world-space point that is relative to the screen

        // using the camera's position from the origin (world-space Vector3(0,0,0)
        //leftConstraint = Camera.main.ScreenToWorldPoint( new Vector3(0.0f, 0.0f, 0 - Camera.main.transform.position.z) ).x;
        //rightConstraint = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, 0.0f, 0 - Camera.main.transform.position.z) ).x;

        // using a specific distance
        LeftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        RightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        BottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        TopConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
        Buffer = 0.6f;
    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        // Exercise
        float horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;

        // Extra
        float vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(horizontal, vertical, 0);

        // Extra
        if (transform.position.x < LeftConstraint - Buffer)
        {
            transform.position = new Vector3(RightConstraint + Buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.x > RightConstraint + Buffer)
        {
            transform.position = new Vector3(LeftConstraint - Buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.y > TopConstraint + Buffer)
        {
            transform.position = new Vector3(transform.position.x, BottomConstraint - Buffer, transform.position.z);
        }

        if (transform.position.y < BottomConstraint - Buffer)
        {
            transform.position = new Vector3(transform.position.x, TopConstraint + Buffer, transform.position.z);
        }

        if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        
    }
}