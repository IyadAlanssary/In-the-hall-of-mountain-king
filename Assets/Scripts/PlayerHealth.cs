using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float health = 0f, elapsed = 0f;
    public GameObject[] hearts;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deadHeroPrefab, createdByObject, gameOverObject;
    [SerializeField] private Sprite kingBirdSprite;
    [SerializeField] private RuntimeAnimatorController kingBirdAnimController;
    //private bool winnerWinnerChickenDinner;

    private void Start() {
        //winnerWinnerChickenDinner = savedData.winnerWinnerChickenDinner;
        Debug.Log(savedData.winnerWinnerChickenDinner);
        if(savedData.winnerWinnerChickenDinner){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = kingBirdSprite;
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = kingBirdAnimController;
        }
        health = maxHealth;
        Invoke("winGame", 152); //152 seconds (time of song)
    }
    private void Update() {
        if ( Input.GetKeyDown(KeyCode.O) ){
            UpdateHealth(+1);
        }
        elapsed += Time.deltaTime;
    }

    public void UpdateHealth(float mod){
        if(mod < 0 && elapsed <= 1f){
            return;
        }
        if(mod > 0 && health < maxHealth){
            hearts[( (int) health ) ].SetActive(true);
        }
        else if(mod < 0){
            gameObject.GetComponent<Animator>().SetTrigger("Recovery");
            //Destroy(hearts[((int)health)-1]);
            hearts[((int)health)-1].SetActive(false);

        }
        
        
        elapsed = 0f;
        
        health += mod;
        
        if(health > maxHealth)
            health = maxHealth;
        else if ( health <= 0 ){
            //AudioListener.pause = true;
            //AudacityLabelAlert a = new AudacityLabelAlert();
            //a.music.Stop();
            AudacityLabelAlert.stopMusic();
            AudacityLabelAlert.playBirdDied();
            //birdDie.Play();
            
            gameOverObject.SetActive(true);
            savedData.winnerWinnerChickenDinner = false;
            health = 0;
            Instantiate(deadHeroPrefab, gameObject.transform.position, Quaternion.identity);
            Time.timeScale = 0;
            Destroy(this.gameObject);
            
         }
    }
    private void winGame(){
        if(this.gameObject.activeInHierarchy){
            //AudioListener.pause = true;
            AudacityLabelAlert.playBirdWin();
            //birdWin.Play();
            createdByObject.SetActive(true);
            savedData.winnerWinnerChickenDinner = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = kingBirdSprite;
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = kingBirdAnimController;
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Time.timeScale = 0;
        }
    }
}
