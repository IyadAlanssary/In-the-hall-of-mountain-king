using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    public GameObject[] hearts;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deadHeroPrefab;

    private void Start() {
        health = maxHealth;
    }
    private void Update() {
        if ( Input.GetKeyDown(KeyCode.O) ){
            UpdateHealth(+1);
        }
    }

    public void UpdateHealth(float mod){
        if(mod<0){
            Destroy(hearts[((int)health)-1]);
        }
        health += mod;
        
        if(health>maxHealth)
            health=maxHealth;
        else if (health<=0){
            health = 0;
            Debug.Log("Player Respawn");
            Instantiate(deadHeroPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
         }
    }
}
