using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float speed;

    public Transform izquierda;
    public Transform derecha;
    public LayerMask pared;

    private float direction;

    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
    }
    private bool isFalling()
    {
        return rb2D.velocity.y < 0;
    }
    void SwitchDirection()
    {
        direction = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SwitchDirection();
        }
    }

    private bool IsChocado()
    {
        if (!isFalling())
        {
            return Physics2D.OverlapArea(izquierda.position, derecha.position, pared);
        }
        else return false;
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Mathf.Abs(speed) * direction, rb2D.velocity.y);

        //si se choca
        if (IsChocado())
        {
            SwitchDirection();
        }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
