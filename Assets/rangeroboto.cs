using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeroboto : MonoBehaviour
{
    private Animator animator;
    public Transform spritePlayer;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        animator = spritePlayer.GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
    }
    //faz roboto saber se você está no range do "range"
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("range"))
        {
         
                GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
        }


        //faz roboto ir até você, duh
    }
}
