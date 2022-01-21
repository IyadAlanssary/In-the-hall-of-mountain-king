using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float movementSpeed;    
    float dx;
    Vector2 movement;
    
    void Update(){
      movement.x = Input.GetAxisRaw("Horizontal");
    }


    void FixedUpdate(){
      rb.MovePosition(rb.position +  movement * movementSpeed *Time.fixedDeltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.tag.Equals("Enemy"))
        gameObject.GetComponent<PlayerHealth>().UpdateHealth(-1);
        //PlayerHealth.UpdateHealth(-1);
    }


    //rb.velocity = movement;    
    /*         
      ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
      em.enabled = true;
      else
      em.enabled = false;
    */
  
}
