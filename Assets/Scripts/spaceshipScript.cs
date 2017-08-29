using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spaceshipScript : MonoBehaviour
{
    public int Speed = 30;
    public int RotationSpeed = 200;
    public float Inertia = 0.5f;
    public GameObject BulletObject;
    public double BulletDistanceFromSpaceship = 0.5;
    public int SmoothTime = 50;

    // World Constraints
    private float _leftConstraint;

    //Não é javascript!!!! hhehehehehe
    private float _rightConstraint;
    private float _topConstraint;
    private float _bottomConstraint;
    private bool _shouldShoot;
    private bool _inertial;
    private Vector3 _velocity;
    private float _time;
    private bool _shouldInertia;

    private Rigidbody2D _rigidbody2D;

    // Object tolerance and Camera Z position
    private const float Buffer = 0.41f;

    private const float DistanceZ = 1.0f;


    private void OnBecameInvisible()
    {
        _shouldShoot = false;
    }

    private void OnBecameVisible()
    {
        _shouldShoot = true;
    }

    void Awake()
    {
        //http://answers.unity3d.com/questions/276836/fall-off-left-side-of-screen-and-spawn-on-right.html
        _leftConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, DistanceZ)).x;
        _rightConstraint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, DistanceZ)).x;
        _bottomConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, DistanceZ)).y;
        _topConstraint = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, DistanceZ)).y;
        //GetComponent<Rigidbody2D>().inertia = Inertia;
        this._rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        this._rigidbody2D.AddRelativeForce(Vector2.up * Input.GetAxis("Vertical") * Speed * Time.deltaTime);
        //transform.Translate(0, vertical, 0);
        transform.Rotate(0, 0, -horizontal);



        //Calculate if Spaceship is inside boundaries and if not, make it appear on the opposite side.
        CheckBoundaries();

        // Shoot
        if (Input.GetKeyDown("space"))
        {
            ShootBullet();
        }

        // Inertial Movement
       // if (!Input.GetKeyUp("up") && !Input.GetKeyUp("down") && !Input.GetKeyUp("left") &&
         //   !Input.GetKeyUp("right")) return;

        //InertialMovement(vertical);
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


 /*   private void InertialMovement(float y)
    {
        _velocity = new Vector2(0, y).normalized * Time.deltaTime * SmoothTime;
        Vector2 move = _velocity * SmoothTime;

        Debug.Log(move.ToString());
        
        GetComponent<Rigidbody2D>().AddForce(move);
    }*/

    private void ShootBullet()
    {
        if (_shouldShoot)
        {
            Instantiate(BulletObject, transform.position, transform.rotation);
        }
    }
}