using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] private float maxHealth;

    private void Start() {
        health = maxHealth;
    }

    public void UpdateHealth(float mod){
        health += mod;
        
        if(health>maxHealth)
            health=maxHealth;
        else if (health<=0){
            health = 0;
            Debug.Log("Player Respawn");
            Destroy(this.gameObject);
         }
         Debug.Log("Player Health: "+ health);
    }
}
