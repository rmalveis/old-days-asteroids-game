using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int Speed = 6;

    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, Speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}