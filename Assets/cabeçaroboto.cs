using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabeçaroboto : MonoBehaviour
{
    private bool jumpcount;
    private float jumpHeight = 5;
    private float moveSpeed = 1;
    Animator animator;
    Rigidbody2D rb2d;
    private Transform player;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    { 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("pisado", true);
            gameObject.GetComponent<Collider2D>().enabled = false;
            transform.parent.gameObject.GetComponent<Collider2D>().enabled = false;
            transform.parent.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(rb2d);
            Destroy(transform.parent.gameObject, 3);

        }
    }
}