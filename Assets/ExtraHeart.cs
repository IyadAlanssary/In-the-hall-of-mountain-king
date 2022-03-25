using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeart : MonoBehaviour{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float speed;
    private float t, ySpeed;//pi2 = 6.285f
    void Start(){
        t = 0;
        ySpeed = 1.3f * speed; // changeable
    }
    
    private Vector3 _newPosition;
    
    void Update(){
        t += 0.03f;
        _newPosition = transform.position;
        _newPosition .y += ySpeed * Mathf.Sin(t) * Time.deltaTime;//sin(Time.time)  //issue that when the game is sped up
                                                                                        //the y is too high
        _newPosition .x -= speed * Time.deltaTime;
        transform.position = _newPosition ;

        if(this.gameObject.transform.position.x <= -9.3f)
            Destroy(this.gameObject);
        
    }  

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(+1);
            Destroy(this.gameObject);
        }
    }
}
