using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float HP = 3;//ainda não usei
    public float angulomacaco = 0;
    private int maxHP; //ainda não usei
    private bool travamovimento = false;
    private bool diagonal1 = false;
    private Rigidbody2D rb2d;
    public float velocidade = 4;
    public bool jumpcount;
    public float forcaPulo;
    private bool estaNoChao;
    public Transform chaoVerificador;
    public Transform spritePlayer;
    private Animator animator;
    public bool direita;
    public GameObject tiro;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = spritePlayer.GetComponent<Animator>();
    }

    void Update()
    {
        travamovimentoif();
        Movimentacao();
        Movimentacaoesq();
        tirol();
        inimigohitbox();
    }


    //trava o movimento enquanto leva dano
    void travamovimentoif()
    {
        if (rb2d.velocity.y == 0.0 && rb2d.velocity.x == 0.0 && travamovimento == true)
        {
            travamovimento = false;
        }
    }
    //O que acontece quando você bate num inimigo e o int pro HP
    void inimigohitbox()
    {

    }
    //Solta o poder
    void tirol ()
    {

    }
    //Responsavel pela movimentacao do personagem
    void Movimentacaoesq()
    //Anda para a esquerda
    //Eu não sei muito bem por que eu dividi isso em 2 funções diferentes
    {
        if (Input.GetAxisRaw("Horizontal") < 0 && travamovimento == false)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 180, -angulomacaco -2);
            direita = false;
            transform.gameObject.tag = "rangeesquerda";
            velocidade = velocidade + (13 * Time.deltaTime);
            if (velocidade > 13)
            { velocidade = 13; }

        }
        else
        {
            if (direita == false)
            {
                if (velocidade > 4)
                {
                    velocidade = velocidade + (-20 * Time.deltaTime);
                }
                else
                { velocidade = 4; }
            }
        }

    }
    void Movimentacao()
    {
        //Seta no paramentro movimento, um valor 0 ou maior que 0. Ira ativar a condicao para mudar de animacao
        animator.SetFloat("movimento", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        //Anda para a direita
        {
            if (Input.GetAxisRaw("Horizontal") > 0 && travamovimento == false)
            {
                transform.Translate(Vector2.right * velocidade * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, angulomacaco);
                direita = true;
                transform.gameObject.tag = "range";
                velocidade = velocidade + (13 * Time.deltaTime);
                if (velocidade > 13)
                { velocidade = 13; }

            }
            else
            {
                if (direita == true)
                {
                    if (velocidade > 4)
                    {
                        velocidade = velocidade + (-20 * Time.deltaTime);
                    }
                    else
                    { velocidade = 4; }
                }
            }
        }
        //Verifica se esta no chao. Comentei pois não é mais útil
        //estaNoChao = Physics2D.Linecast(transform.position, chaoVerificador.position, 1 << LayerMask.NameToLayer("Piso"));
        //animator.SetBool("chao", estaNoChao);

        //Responsavel pelo pulo
        if (Input.GetButtonDown("Jump") && jumpcount == true && travamovimento == false)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * forcaPulo);
            jumpcount = false;
        }
            if (rb2d.velocity.y > 0.0)
        {
            jumpcount = false;
            animator.SetBool("nomovimentoy", false);
            animator.SetBool("chao", false);
        }
        if (rb2d.velocity.y < 0.0)
        {
            jumpcount = false;
            animator.SetBool("nomovimentoy", false);
            animator.SetBool("chao", false);
           // if ()
        }
        if (rb2d.velocity.y == 0.0)
        {
            jumpcount = true;
            animator.SetBool("nomovimentoy", true);
            animator.SetBool("chao", true);

        }
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("chaodiagonal1") && diagonal1 == false)
        {
            rb2d.rotation = 20;
            angulomacaco = 20;
            diagonal1 = true;

        }

        if (other.gameObject.CompareTag("Chao"))
        {
            rb2d.rotation = 0;
            angulomacaco = 0;
            diagonal1 = false;

        }
        if (other.gameObject.CompareTag("inimigohitbox") && direita == true )
        {
            rb2d.velocity = new Vector3(0, 0, 0);
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7, ForceMode2D.Impulse);
            travamovimento = true;
            velocidade = 4;
            //Manda personagem pra tras
            HP = HP - 1;
            if (HP == 0)
            {
                Debug.Log("MORREU", gameObject);
            }
            //Tira HP
        }
        if (other.gameObject.CompareTag("inimigohitbox") && direita == false)
        {
            rb2d.velocity = new Vector3(0, 0, 0);
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7, ForceMode2D.Impulse);
            travamovimento = true;
            velocidade = 4;
            //Manda personagem pra tras
            HP = HP - 1;
            if (HP < 1)
            {
                Debug.Log("MORREU", gameObject);
            }
            //Tira HP
        }
        if (other.gameObject.CompareTag("dano"))
        {
            rb2d.velocity = new Vector3(0, 0, 0);
            transform.Translate(Vector2.left * 55 * Time.deltaTime);
            GetComponent<AudioSource>().Play();
           //Verifica se esta na cabeça do roboto (ainda não usei)
        }
    }
}
    