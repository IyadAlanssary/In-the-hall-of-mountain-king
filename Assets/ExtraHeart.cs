using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeart : MonoBehaviour{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float speed;
    private float t, ySpeed;//pi2 = 6.285f
    private int randomSinOrCos;
    void Start(){
        t = 0;
        ySpeed = 0.8f * speed; // changeable// (hight of heart (y pos))
        randomSinOrCos = Random.Range(0, 2);
    }
    
    private Vector3 _newPosition;
    private void FixedUpdate() {
        t += 0.07f;
        _newPosition = transform.position;
        if(randomSinOrCos == 0){    
            _newPosition .y += ySpeed * Mathf.Sin(t) * Time.fixedDeltaTime;//was sin(Time.time)
        }
        else{
            _newPosition .y += ySpeed * Mathf.Cos(t) * Time.fixedDeltaTime;
        }
        _newPosition .x -= speed * Time.fixedDeltaTime;
        transform.position = _newPosition ;

        if(this.gameObject.transform.position.x <= -9.3f)
            Destroy(this.gameObject);
        
        if(Input.GetKeyDown(KeyCode.H) && !PauseMenu.isPaused){
            if(ySpeed == 0.8f * speed)  //because when pressing h the timescale is 2 >> which make Time.deltatime
                ySpeed = 0.4f * speed;  //     half its value >> so ySpeed is changed to keep y pos of heart the same
            else
                ySpeed = 0.8f * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(+1);
            Destroy(this.gameObject);
        }
    }
}
