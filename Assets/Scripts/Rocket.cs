using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float attackDamage;
    public Rigidbody2D Rigidbody;
    private void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + Vector2.up * Speed * Time.fixedDeltaTime);    
        
        if (transform.position.y > 7){
          Destroy(this.gameObject);
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) {
      if(other.gameObject.tag.Equals("Player")){
        other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
        Destroy(this.gameObject);
        
      }
    }
}
