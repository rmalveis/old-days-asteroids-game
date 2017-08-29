using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spaceshipScript : MonoBehaviour
{
    public int Speed = 100;
    public int RotationSpeed = 200;
    public GameObject BulletObject;

    // World Constraints
    private float _leftConstraint;

    private float _rightConstraint;
    private float _topConstraint;
    private float _bottomConstraint;
    private bool _shouldShoot;
    private Vector3 _velocity;
    private float _time;

    // Object tolerance and Camera Z position
    private const float Buffer = 0.41f;

    private const float DistanceZ = 1.0f;
    private Rigidbody2D _rb;


    private void OnBecameInvisible()
    {
        _shouldShoot = false;
    }

    private void OnBecameVisible()
    {
        _shouldShoot = true;
    }

    private void Awake()
    {
        //http://answers.unity3d.com/questions/276836/fall-off-left-side-of-screen-and-spawn-on-right.html
        _leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, DistanceZ)).x;
        _rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, DistanceZ)).x;
        _bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, DistanceZ)).y;
        _topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, DistanceZ)).y;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckBoundaries();

        // Shoot
        if (Input.GetKeyDown("space"))
        {
            ShootBullet();
        }

        // Inertial Movement
        if (!Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey("left") &&
            !Input.GetKey("right")) return;

        var horizontal = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
        var force = Vector2.up * Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        _rb.AddRelativeForce(force);
        transform.Rotate(0, 0, -horizontal);
    }

    private void CheckBoundaries()
    {
        if (transform.position.x < _leftConstraint - Buffer)
        {
            transform.position = new Vector3(_rightConstraint + Buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.x > _rightConstraint + Buffer)
        {
            transform.position = new Vector3(_leftConstraint - Buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _topConstraint + Buffer)
        {
            transform.position = new Vector3(transform.position.x, _bottomConstraint - Buffer, transform.position.z);
        }

        if (transform.position.y < _bottomConstraint - Buffer)
        {
            transform.position = new Vector3(transform.position.x, _topConstraint + Buffer, transform.position.z);
        }
    }

    private void ShootBullet()
    {
        if (_shouldShoot)
        {
            Instantiate(BulletObject, transform.position, transform.rotation);
        }
    }
}