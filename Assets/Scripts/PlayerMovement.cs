using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    private float dx, flippedXScale, xScale, elapsed = 0f;
    Vector2 movement;
    private Animator animator;

    private void Start()
    {
        xScale = transform.localScale.x;
        flippedXScale = -1 * xScale;
        animator = gameObject.GetComponent<Animator>();
        animator.speed = 0.4f;
        InvokeRepeating("animatorSpeed", 20, 20);
    }
    void animatorSpeed()
    {
        animator.speed += 0.2f;
    }
    void Update()
    {
        elapsed += Time.deltaTime;
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(flippedXScale, transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && elapsed >= 0.5f)
        {
            gameObject.GetComponent<PlayerHealth>().UpdateHealth(-1);
            elapsed = 0f;
        }
    }

    //rb.velocity = movement;    
    /*         
      ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
      em.enabled = true;
      else
      em.enabled = false;
    */
}
