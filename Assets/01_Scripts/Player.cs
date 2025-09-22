using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;         
    public float jumpForce = 12f; 
    public float gravity = -25f; 

    public LayerMask groundLayer;    
    public float skinWidth = 0.05f;  
    public float rayLength = 0.1f;   

    private Vector2 velocity;       
    private bool isGrounded;
    public Animator animator;
    public Transform groundCheck;

    public int life = 5;
    public int currentHeald;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHeald = life;
    }
    void Update()
    {
        Movement();
        takeObjetMortal();
    }
    public void Movement()
    {
        float dt = Time.deltaTime;

        float x = Input.GetAxis("Horizontal") * speed;
        velocity.x = x;
        animator.SetFloat("Horizontal", Mathf.Abs(x));
        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        velocity.y += gravity * dt;//aplicar gravedad

        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength + skinWidth, groundLayer);

        if (hit.collider != null && velocity.y <= 0)
        {
            isGrounded = true;


            float distToGround = hit.distance - skinWidth;
            if (distToGround > 0f)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - distToGround);
            }
            velocity.y = 0f;
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("inwall", isGrounded);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        transform.Translate(velocity * dt);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * (rayLength + skinWidth));
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log("Dano player "+life);
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }
    public void takeObjetMortal()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, LayerMask.GetMask("DeathObstacle"));
        if (hit.collider != null)
        {
            Destroy(gameObject);
        }
    }
}
