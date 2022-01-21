using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float Speed;

    void Start()
    {

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * Speed * Time.fixedDeltaTime);
        if (rb.position.x >9){
            Destroy(this.gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag== "Enemy" ){
            Destroy(other.gameObject);
            Destroy(this.gameObject);  
        }
        else if(other.gameObject.tag != "Player"){
            Destroy(this.gameObject); 
        }
    }
}
