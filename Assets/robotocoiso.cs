using UnityEngine;
using System.Collections;

public class robotocoiso : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float velocidade;
    public bool jumpcount;
    public float forcaPulo;
    private bool estaNoChao;
    public Transform chaoVerificador;
    public Transform spritePlayer;
    private Animator animator;

    //Tudo que ocorre quando o personagem e criado
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        animator = spritePlayer.GetComponent<Animator>();
    }


    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Chao"))
        {
            jumpcount = true;
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.CompareTag("pisada"))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * forcaPulo * 1);
            //Verifica se esta na cabeça do roboto

        }
    }


}