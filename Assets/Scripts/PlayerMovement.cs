using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    private float dx, flippedXScale, xScale;
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
        //touch input
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            if(touchPosition.x > 0){
                movement.x = 1;
                transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            }
            else{
                movement.x = -1;
                transform.localScale = new Vector3(flippedXScale, transform.localScale.y, transform.localScale.z);
            }
        }
        else{
            movement.x = 0;
        }

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
    public void phoneInputRight(){
        // movement.x = 1;
        // transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
    public void phoneInputLeft(){
        // movement.x = -1;
        // transform.localScale = new Vector3(flippedXScale, transform.localScale.y, transform.localScale.z);
    }

    //rb.velocity = movement;    
    /*         
      ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
      em.enabled = true;
      else
      em.enabled = false;
    */
}
