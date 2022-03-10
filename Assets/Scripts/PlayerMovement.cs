using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float movementSpeed;    
    float dx, flippedXScale, xScale;
    Vector2 movement;
    private void Start() {
      xScale = transform.localScale.x;
      flippedXScale = -1 * xScale;
    }
    
    void Update(){
      movement.x = Input.GetAxisRaw("Horizontal");
      if(movement.x > 0){
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
      }
      else if (movement.x < 0){
        transform.localScale = new Vector3(flippedXScale, transform.localScale.y, transform.localScale.z);
      }
    }


    void FixedUpdate(){
      rb.MovePosition(rb.position +  movement * movementSpeed *Time.fixedDeltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag.Equals("Enemy"))
        gameObject.GetComponent<PlayerHealth>().UpdateHealth(-1);
    }


    //rb.velocity = movement;    
    /*         
      ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
      em.enabled = true;
      else
      em.enabled = false;
    */
  
}
