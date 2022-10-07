using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float health = 0f, elapsed = 0f;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deadHeroPrefab, createdByObject, gameOverObject;
    [SerializeField] private Sprite kingBirdSprite;
    [SerializeField] private RuntimeAnimatorController kingBirdAnimController;

    private void Start()
    {
        if (SavedData.winnerWinnerChickenDinner)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = kingBirdSprite;
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = kingBirdAnimController;
        }
        health = maxHealth;
        Invoke("WinGame", 152); //152 seconds (time of song)
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateHealth(+1);
        }
        elapsed += Time.deltaTime;
    }

    public void UpdateHealth(float mod)
    {
        if (mod < 0 && elapsed <= 1f)
        {
            //in recovery time (1 second)
            return;
        }
        if (mod > 0 && health < maxHealth)
        {
            hearts[((int)health)].SetActive(true);
        }
        else if (mod < 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Recovery");
            hearts[((int)health) - 1].SetActive(false);
        }
        elapsed = 0f;

        health += mod;
        if (health > maxHealth)
            health = maxHealth;
        else if (health <= 0)
        {
            LoseGame();
        }
    }
    private void WinGame()
    {
        if (this.gameObject.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().Play("bird win");
            createdByObject.SetActive(true);
            SavedData.winnerWinnerChickenDinner = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = kingBirdSprite;
            this.gameObject.GetComponent<Animator>().runtimeAnimatorController = kingBirdAnimController;
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Time.timeScale = 0;
        }
    }
    private void LoseGame()
    {
        FindObjectOfType<AudioManager>().Stop("in the hall");
        FindObjectOfType<AudioManager>().Play("bird die");
        gameOverObject.SetActive(true);
        SavedData.winnerWinnerChickenDinner = false;
        health = 0;
        Instantiate(deadHeroPrefab, gameObject.transform.position, Quaternion.identity);
        Time.timeScale = 0;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            UpdateHealth(-1);
        }
    }
}
