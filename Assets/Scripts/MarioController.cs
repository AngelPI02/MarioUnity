using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{

    Rigidbody2D rb2D;
    public float jumpForce;
    public float aceleration = 150f;

    private float direction;
    
    private float velocity;

    public float maxSpeed;

    public int lives = 3;
    public Transform top_left;
    public Transform bottom_right;
    public LayerMask suelo;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool facingRight = true;
    
    
    private Animator animator;


    private string horizontal = "Horizontal";

    public AudioSource audioSource;

    public AudioClip audioJjump;
    public AudioClip audioDie;
    


   

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        audioSource.clip = audioJjump;
        jumpTimeCounter = jumpTime;

    }

    private bool isFalling()
    {
        return rb2D.velocity.y <0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Hit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cabeza"))
        { 
            audioSource.Play();

            animator.SetBool("Jumping", true);

            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce*0.5f);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (collision.CompareTag("Win"))
        {
            Win();
        }
    }

    private void Win()
    {
        
        audioSource.clip = audioDie;
        audioSource.Play();
        horizontal = null;
        rb2D.velocity = new Vector2(2, rb2D.velocity.y);

    }

    public void Hit()
    {
        if (!isMarioBig())
        {
            Die();
        }
        else
        {

        }
    }

    public bool isMarioBig()
    {
        return false;
    }

    private void Die()
    {
        
        audioSource.clip = audioDie;
        audioSource.Play();
        animator.SetBool("dead", true);
        rb2D.gravityScale = 0;
        this.GetComponent<Collider2D>().enabled = false;
        horizontal = null;
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(Vector2.up * 3.5f, ForceMode2D.Impulse);
        StartCoroutine(CheckLives());
    }

    private IEnumerator CheckLives()
    {
        
        lives--;
        if (lives == 0)
        {
            //Cambia d escena game over looooooooooooooooooooooooooooooooooooool
            //SceneManager.LoadScene("GameOver");
            SceneManager.LoadScene(0);
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {

        if (isFalling()) {
            rb2D.gravityScale = 1.2f;
        }
        if (!isFalling()) { 
            rb2D.gravityScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce*0.5f);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

    }

    private bool isGrounded()
    {
        return Physics2D.OverlapArea(top_left.position, bottom_right.position, suelo);
    }
    public void Jump()
    {
        if (isGrounded())
        {
            audioSource.Play();

            animator.SetBool("Jumping", true);

            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
    }


    private void FixedUpdate()
    {
        direction = Input.GetAxisRaw(horizontal);
        if (direction != 0)
            velocity = Mathf.Clamp(velocity + direction * aceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        else
            velocity -= velocity * aceleration * Time.deltaTime;

        rb2D.velocity = new Vector2(velocity, rb2D.velocity.y);
        if (direction > 0)
        {
            animator.SetBool("Running", true);
            if (!facingRight)
                Flip();
        }
        else if (direction < 0)
        {
            animator.SetBool("Running", true);
            if (facingRight)
                Flip();
        }
        else
            animator.SetBool("Running", false);

        if (!isJumping && isFalling()) 
            animator.SetBool("Jumping", false);

        if (isGrounded())
            animator.SetBool("Jumping", false);
        else
            animator.SetBool("Jumping", true);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
