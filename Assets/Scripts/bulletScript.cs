using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int Speed = 10;

//    public Vector2 Velocity;
    private Vector2 _velocity;

    private Rigidbody2D _rb;

    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _velocity = new Vector2(0, Speed) * Time.deltaTime * Speed;
        
        //calculates delta position based on velocity and deltaTime
        var deltaPosition = _velocity * Time.deltaTime;
        
        //calculates how much it should translate
        var move = Vector2.up * deltaPosition.y;
        
        //actually translates the object
        _rb.transform.Translate(move);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}