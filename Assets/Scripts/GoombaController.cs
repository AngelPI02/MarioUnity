using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;

    public Transform izquierda;
    public Transform derecha;
    public LayerMask pared;



    private Animator animator;


    private float direction;

    private MarioController marioController;

    public float tiempoEntreLlamadas = 0.01f;
    private float ultimoLlamado = 0f;

    public AudioSource audioSource;
    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
        animator = GetComponent<Animator>();
        marioController = GameObject.FindObjectOfType<MarioController>();
        audioSource = GameObject.FindObjectOfType<AudioSource>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private bool isFalling()
    {
            return rb2D.velocity.y<0;
    }
    private bool IsChocado()
    {
        if (!isFalling())
        {
            return Physics2D.OverlapArea(izquierda.position, derecha.position, pared);
        }
        else return false;
    }
    void SwitchDirection() { 
            direction = -direction;
    }


    public void CosasBonitas() {
        animator.SetBool("dead", true);
            audioSource.Play();

    }

    private void Awake()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        rb2D.velocity = Vector2.zero;
        rb2D.Sleep();
    }

    private void OnEnable()
    {
        rb2D.WakeUp();
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    public void Die() {
        // se le cambia el tag para no provocar que mario sufra daño y suena el audio
        // se le de quita el collider para no molestar y la gravedad y la speed para que se quede quieto, 
        // ajustes de animacion y se baja un poco para dar la sensacion de que cae al suelo y despues de medio segundo se elimina
        DisablePhysics();
        CosasBonitas();
        GameManager.Instance.AddPoints(100);
        Destroy(gameObject, 0.5f);

    }


    public void DisablePhysics() {
        this.tag = "Untagged";
        this.GetComponent<Collider2D>().enabled = false;
        rb2D.gravityScale = 0;
        speed = 0;
        rb2D.velocity = Vector2.zero;
        Vector2 ve = new Vector2(transform.position.x, transform.position.y - 0.05f);
        transform.position = ve;
        this.enabled = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mario"))
        {
            muero();
        }
        if (collision.gameObject.CompareTag("Enemy")){ 
            SwitchDirection();
        }

    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Mathf.Abs(speed) * direction, rb2D.velocity.y);
 
        //si se choca
        if (IsChocado() && Time.time - ultimoLlamado > tiempoEntreLlamadas) {
            SwitchDirection();
            ultimoLlamado = Time.time;
        }
    }

    void muero()
    {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        Die();
    }
}
